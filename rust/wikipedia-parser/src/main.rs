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

fn search<'a>(iter: &'a mut scraper::html::Select, parent: &'a mut Headline<'a>, n: i32) {
    let search_tag = format!("h{}", n);
    //     let child_tag = format!("h{}", n + 1);
    while let Some(node) = iter.next() {
        if node.value().name() == search_tag {
            println!("{:?}", node.value().name());
            //                             parent.children.push(Headline::new_with_node(node));
            parent.children.push(Headline::new());
            search(iter, parent.children.last_mut().unwrap(), n + 1);
        }
    }
}

fn get_headlines3(doc: &scraper::Html) {
    let mut ret = Headline::new();
    let selector = Selector::parse("h1,h2,h3,h4,h5,h6").unwrap();
    let mut iter = doc.select(&selector);
    search(&mut iter, &mut ret, 1);
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
    get_headlines3(&doc);

    Ok(())
}
