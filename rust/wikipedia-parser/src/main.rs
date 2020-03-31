extern crate scraper;
use wikipedia_parser::headline;
use wikipedia_parser::param;

use scraper::Html;
use std::fs;

fn main() -> Result<(), Box<dyn std::error::Error>> {
    let p = param::get_param();

    let html = fs::read_to_string(p.filename)?;
    let doc = Html::parse_document(&html);
    let headlines = headline::get_headlines(&doc);
    if p.headlines {
        headline::display_headlines(&headlines);
    }

    Ok(())
}
