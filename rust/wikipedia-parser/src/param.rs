extern crate clap;
use clap::{App, Arg};

pub struct Param {
    pub filename: String,
    pub headlines: bool,
    pub tocnum: String,
}

impl Param {
    fn new() -> Self {
        Self {
            filename: "".to_string(),
            headlines: false,
            tocnum: "".to_string(),
        }
    }
}

pub fn get_param() -> Param {
    let mut p = Param::new();
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
        )
        .arg(
            Arg::with_name("TOCNUMBER")
                .help("TOC Number")
                .long("show")
                .takes_value(true),
        );

    let matches = app.get_matches();
    if let Some(filename) = matches.value_of("FILENAME") {
        p.filename = filename.to_string();
    }

    if let Some(tocnum) = matches.value_of("TOCNUMBER") {
        p.tocnum = tocnum.to_string();
    }

    p.headlines = matches.is_present("headlines");
    return p;
}
