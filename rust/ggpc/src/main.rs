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

fn get_stdout_text(bin: &str, args: Vec<&str>) -> Result<String, Box<dyn std::error::Error>> {
    let mut cmd = Command::new(bin);
    for arg in &args {
        cmd.arg(arg);
    }

    match cmd.output() {
        Ok(o) => match str::from_utf8(&o.stdout) {
            Ok(s) => Ok(s.to_string()),
            Err(err) => Err(Box::new(err)),
        },
        Err(err) => Err(Box::new(err)),
    }
}

fn get_rev_list() -> Result<Vec<String>, Box<dyn std::error::Error>> {
    // FIXME: git が無いディレクトリで実行しても Err に入らない。なんで？
    match get_stdout_text("git", vec!["rev-list", "HEAD"]) {
        Ok(s) => Ok(s.split('\n').map(|x| x.to_string()).collect()),
        Err(err) => Err(err),
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

    match get_rev_list() {
        Ok(l) => println!("OK: {:?}", l),
        Err(err) => println!("{}", err),
    }
}
