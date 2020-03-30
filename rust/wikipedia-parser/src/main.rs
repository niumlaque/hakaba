extern crate clap;
extern crate scraper;
use clap::{App, Arg};
use scraper::{Html, Selector};
use std::collections::HashMap;
use std::fs;

struct Param {
    filename: String,
    headlines: bool,
}

impl Param {
    fn new() -> Self {
        Self {
            filename: "".to_string(),
            headlines: false,
        }
    }
}

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

fn display_headlines(headlines: &HashMap<String, Headline>) {
    let mut sorted: Vec<_> = headlines.into_iter().collect();
    sorted.sort_by(|x, y| x.0.cmp(&y.0));
    let sorted = sorted;
    // 単に文字列の比較だとまずい
    for headline in sorted.iter().map(|x| x.1) {
        let count = count_char(&headline.index, '.');
        println!("{}{} {}", "  ".repeat(count), headline.index, headline.text);
    }
}

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let mut param = Param::new();
    let app = App::new("wikipedia-parser")
        .version("0.1.0")
        .author("Niumlaque")
        .arg(
            Arg::with_name("FILENAME")
                .help("Path to file")
                .required(true),
        )
        .arg(
            Arg::with_name("headlines")
                .help("Display headlines")
                .long("headlines"),
        );

    let matches = app.get_matches();
    if let Some(filename) = matches.value_of("FILENAME") {
        param.filename = filename.to_string();
    }

    param.headlines = matches.is_present("headlines");
    let param = param;
    let html = fs::read_to_string(param.filename)?;
    let doc = Html::parse_document(&html);
    let headlines = get_headlines(&doc);
    if param.headlines {
        display_headlines(&headlines);
    }

    Ok(())
}
