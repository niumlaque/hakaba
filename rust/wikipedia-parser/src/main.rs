extern crate scraper;
use wikipedia_parser::param;

use scraper::{Html, Selector};
use std::collections::HashMap;
use std::fs;

#[derive(Debug)]
struct Headline {
    index: String,
    text: String,
    href: String,
}

impl Headline {
    fn new(index: &str, text: &str, href: &str) -> Self {
        Self {
            index: index.to_string(),
            text: text.to_string(),
            href: href.to_string(),
        }
    }
}

fn get_tocnumber(elm: scraper::ElementRef) -> Option<String> {
    let span = Selector::parse("span.tocnumber").unwrap();
    match elm.select(&span).next() {
        Some(tocnumber) => Some(tocnumber.inner_html()),
        _ => None,
    }
}

fn get_toctext(elm: scraper::ElementRef) -> Option<String> {
    let span = Selector::parse("span.toctext").unwrap();
    match elm.select(&span).next() {
        Some(toctext) => Some(toctext.inner_html()),
        _ => None,
    }
}

// toclevel 1 の li をパースする
fn parse_top_section(elm: scraper::ElementRef, ret: &mut HashMap<String, Headline>) {
    let a = Selector::parse("a").unwrap();
    for node in elm.select(&a) {
        if let Some(href) = node.value().attr("href") {
            if let Some(index) = get_tocnumber(node) {
                if let Some(text) = get_toctext(node) {
                    ret.insert(index.to_string(), Headline::new(&index, &text, &href));
                }
            }
        }

        // // return したくないんだけどしちゃう。if 並べるしかない？
        //         match (
        //             node.value().attr("href"),
        //             get_tocnumber(node),
        //             get_toctext(node),
        //         ) {
        //             (Some(href), Some(index), Some(text)) => {
        //                 ret.insert(index.to_string(), Headline::new(&index, &text, &href))
        //             }
        //             _ => (),
        //         }
    }
}

fn search_top_section(elm: scraper::ElementRef, ret: &mut HashMap<String, Headline>) {
    let li = Selector::parse("li.toclevel-1").unwrap();
    for node in elm.select(&li) {
        parse_top_section(node, ret);
    }
}

fn get_headlines(doc: &scraper::Html) -> HashMap<String, Headline> {
    let mut ret = HashMap::new();
    let root = Selector::parse("div.toc > ul").unwrap();
    for node in doc.select(&root) {
        search_top_section(node, &mut ret);
    }

    return ret;
}

fn count_char(s: &str, ch: char) -> usize {
    let mut count: usize = 0;
    let chars = s.chars();
    for x in chars {
        if x == ch {
            count = count + 1;
        }
    }

    return count;
}

fn sort_by_tocnumber(tocnumbers: &Vec<&str>) -> Vec<String> {
    let mut prepare: Vec<Vec<i32>> = vec![];
    let mut max = 0;
    for number in tocnumbers {
        let splited = number
            .split('.')
            .map(|x| x.parse().unwrap())
            .collect::<Vec<i32>>();
        if splited.len() > max {
            max = splited.len();
        }

        prepare.push(splited);
    }
    for i in (0..=max).rev() {
        prepare.sort_by(|x, y| {
            let xp = if i < x.len() { x[i] } else { 0 };
            let yp = if i < y.len() { y[i] } else { 0 };
            return xp.cmp(&yp);
        });
    }

    let mut ret = vec![];
    for v in prepare {
        ret.push(format!(
            "{}",
            v.into_iter()
                .map(|x| x.to_string())
                .collect::<Vec<String>>()
                .join(".")
        ));
    }

    return ret;
}

fn display_headlines(headlines: &HashMap<String, Headline>) {
    let tocnumbers: Vec<&str> = headlines
        .into_iter()
        .map(|x| x.0)
        .map(AsRef::as_ref)
        .collect();
    let sorted_tocnumbers = sort_by_tocnumber(&tocnumbers);
    for tocnumber in sorted_tocnumbers {
        let count = count_char(&tocnumber, '.');
        println!(
            "{}{} {}",
            "  ".repeat(count),
            tocnumber,
            headlines[&tocnumber.to_string()].text
        );
    }
}

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let p = param::get_param();

    let html = fs::read_to_string(p.filename)?;
    let doc = Html::parse_document(&html);
    let headlines = get_headlines(&doc);
    if p.headlines {
        display_headlines(&headlines);
    }

    Ok(())
}
