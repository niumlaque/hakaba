extern crate scraper;
use wikipedia_parser::content;
use wikipedia_parser::headline;
use wikipedia_parser::param;

use scraper::Html;
use std::fs;

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let p = param::get_param();

    let html = fs::read_to_string(p.filename)?;
    let doc = Html::parse_document(&html);

    let headlines = headline::parse(&doc)?;
    if p.headlines {
        println!("{}", headlines.format_string());
    } else if p.tocnum.len() > 0 {
        match headlines.get_href(&p.tocnum) {
            Some(href) => {
                let _ = content::parse(&doc, href)?;
            }
            None => println!("Not found"),
        }
    }

    Ok(())
}
