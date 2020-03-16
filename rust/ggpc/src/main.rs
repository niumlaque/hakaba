use std::fmt;
use std::process::Command;
use std::str;
extern crate clap;
use clap::{App, Arg};

#[derive(Debug)]
struct Parameters {
    verbose: bool,
    all: bool,
}

impl Parameters {
    fn new() -> Self {
        Self {
            verbose: false,
            all: false,
        }
    }
}

impl fmt::Display for Parameters {
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
        write!(f, "verbose={}, all={}", self.verbose, self.all)
    }
}

fn main() {
    let mut param = Parameters::new();

    let app = App::new("ggpc")
        .version("0.1.0")
        .author("Niumlaque <niumlaque@gmail.com>")
        .about("Grep Git's Past Commit")
        .arg(
            Arg::with_name("verbose")
                .help("Verbose output")
                .short("v")
                .long("verbose"),
        )
        .arg(
            Arg::with_name("all")
                .help("Grep all commits")
                .short("a")
                .long("all"),
        );

    let matches = app.get_matches();
    param.verbose = matches.is_present("verbose");
    param.all = matches.is_present("all");

    if param.verbose {
        println!("{}", param);
    }

    // コマンド試してみる
    match Command::new("git").arg("show").output() {
        Ok(o) => println!("ok"), //println!("{}", str::from_utf8(&o.stdout).unwrap()),
        Err(err) => println!("{}", err),
    }
}
