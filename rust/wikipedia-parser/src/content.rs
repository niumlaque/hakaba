use scraper::Selector;

pub struct ContentParsed<'a> {
    headline: String,
    hnode: Option<scraper::ElementRef<'a>>,
    contents: Vec<scraper::ElementRef<'a>>,
}

impl<'a> ContentParsed<'a> {
    fn new() -> Self {
        Self {
            headline: "".to_string(),
            hnode: None,
            contents: vec![],
        }
    }
}

pub fn parse<'a>(doc: &'a scraper::Html, id: &str) -> Result<ContentParsed<'a>, String> {
    let mut ret = ContentParsed::new();
    let hselector = Selector::parse(id).map_err(|_| "ParseError")?;
    match doc.select(&hselector).next() {
        Some(node) => {
            ret.headline = node.inner_html();
            ret.hnode = Some(node);
        }
        None => return Err("Not found".to_string()),
    }

    match ret.hnode.unwrap().parent() {
        Some(parent) => {
            //             while let Some(sibling) = parent.next_sibling() {
            //                 println!("ok");
            //                 if sibling.value().is_element() {
            //                     println!("{}", ret.contents.last().unwrap().inner_html());
            //                 }
            //             }
        }
        None => println!("ine"),
    }

    Ok(ret)
}
