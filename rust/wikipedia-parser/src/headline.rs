extern crate scraper;
use scraper::Selector;
use std::collections::HashMap;

#[derive(Debug)]
pub struct HeadlineParsed {
    headlines: HashMap<String, Headline>,
}

impl HeadlineParsed {
    pub fn new(headlines: HashMap<String, Headline>) -> Self {
        Self {
            headlines: headlines,
        }
    }

    pub fn format_string(&self) -> String {
        let mut list = vec![];
        let tocnumbers: Vec<&str> = (&self.headlines)
            .into_iter()
            .map(|x| x.0)
            .map(AsRef::as_ref)
            .collect();
        let sorted_tocnumbers = sort_by_tocnumber(&tocnumbers);
        for tocnumber in sorted_tocnumbers {
            let count = count_char(&tocnumber, '.');
            list.push(format!(
                "{}{} {}",
                "  ".repeat(count),
                tocnumber,
                self.headlines[&tocnumber.to_string()].text
            ));
        }

        return list.join("\n");
    }

    pub fn get_href(&self, index: &str) -> Option<&str> {
        match self.headlines.get(index) {
            Some(headline) => Some(&headline.href),
            None => None,
        }
    }
}

#[derive(Debug)]
pub struct Headline {
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
fn parse_top_section(
    elm: scraper::ElementRef,
    ret: &mut HashMap<String, Headline>,
) -> Result<(), String> {
    let a = Selector::parse("a").map_err(|_| "Parse Error")?;
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

    Ok(())
}

fn search_top_section(
    elm: scraper::ElementRef,
    ret: &mut HashMap<String, Headline>,
) -> Result<(), String> {
    let li = Selector::parse("li.toclevel-1").map_err(|_| "Parse Error")?;
    for node in elm.select(&li) {
        parse_top_section(node, ret)?;
    }

    Ok(())
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
            .map(|x| x.parse().unwrap()) // TODO: return Result
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

pub fn parse(doc: &scraper::Html) -> Result<HeadlineParsed, String> {
    let mut ret = HashMap::new();
    let root = Selector::parse("div.toc > ul").map_err(|_| "Parse Error")?;
    //     let root = Selector::parse("div.toc > ul").map_err(|err| String::as_str)?;
    for node in doc.select(&root) {
        search_top_section(node, &mut ret)?;
    }

    Ok(HeadlineParsed::new(ret))
}
