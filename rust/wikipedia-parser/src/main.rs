extern crate clap;
extern crate scraper;
use clap::{App, Arg};
use scraper::{Html, Selector};
use std::fmt;
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
struct Headline<'a> {
    node: Option<scraper::ElementRef<'a>>,
    children: Vec<Headline<'a>>,
}

impl<'a> Headline<'a> {
    fn new() -> Self {
        Self {
            node: None,
            children: vec![],
        }
    }

    fn new_with_node(node: scraper::ElementRef<'a>) -> Self {
        Self {
            node: Some(node),
            children: vec![],
        }
    }
}

impl<'a> fmt::Display for Headline<'a> {
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
        write!(f, "再帰的に書くには？")
    }
}

fn get_headlines2(doc: &scraper::Html) -> Option<Headline> {
    let mut ret = Headline::new();
    for node in doc.select(&Selector::parse("h1").unwrap()) {
        match node.value().attr("id") {
            Some("firstHeading") => {
                ret.node = Some(node);
                break;
            }

            _ => (),
        }
    }

    if ret.node == None {
        return None;
    }

    let selector = Selector::parse("h2>span").unwrap();
    for node in doc.select(&selector) {
        match node.value().attr("class") {
            Some("mw-headline") => ret.children.push(Headline::new_with_node(node)),
            _ => (),
        }
    }

    Some(ret)
}

fn get_headlines(doc: &scraper::Html) -> Option<Vec<scraper::ElementRef>> {
    let mut ret = vec![];
    let selector = Selector::parse("h2>span").unwrap();
    for node in doc.select(&selector) {
        match node.value().attr("class") {
            Some("mw-headline") => ret.push(node),
            _ => (),
        }
    }

    if ret.len() > 0 {
        Some(ret)
    } else {
        None
    }
}

fn get_content(
    doc: &scraper::Html,
    from: &scraper::ElementRef,
    to: &scraper::ElementRef,
) -> Option<String> {
    None
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
    match get_headlines2(&doc) {
        Some(v) => println!("{}", v),
        _ => (),
    }

    let headlines;
    match get_headlines(&doc) {
        Some(v) => headlines = v,
        None => {
            println!("No headlines");
            return Ok(());
        }
    }

    if param.headlines {
        for (i, x) in headlines.iter().enumerate() {
            println!("{}: {}", i + 1, x.inner_html())
        }
    }

    Ok(())
}
