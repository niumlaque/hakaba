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

// 標準出力のテキストを取得する。
fn get_stdout_text(bin: &str, args: Vec<&str>) -> Result<String, Box<dyn std::error::Error>> {
    let mut cmd = Command::new(bin);
    for arg in &args {
        cmd.arg(arg);
    }

    // output に status, stdout, stderr 全部入ってる
    match cmd.output() {
        Ok(o) => match o.status.success() {
            true => match str::from_utf8(&o.stdout) {
                Ok(s) => Ok(s.to_string()),
                Err(err) => Err(Box::new(err)),
            },
            _ => match str::from_utf8(&o.stderr) {
                Ok(s) => Err(From::from(s.to_string())),
                Err(err) => Err(Box::new(err)),
            },
        },

        Err(err) => Err(Box::new(err)),
    }
}

// rev-list のハッシュ値の Vec を取得する。
fn get_rev_list(all: bool) -> Result<Vec<String>, Box<dyn std::error::Error>> {
    let mut args = vec!["rev-list", "HEAD", "--no-merges"];
    if all {
        args.push("--all");
    }

    match get_stdout_text("git", args) {
        Ok(s) => Ok(s
            .split('\n')
            .filter(|x| x.len() > 0)
            .map(|x| x.to_string())
            .collect()),
        Err(err) => Err(err),
    }
}

// 追加･更新があったファイル一覧を取得する。
fn get_files(hash: &str) -> Result<Vec<String>, Box<dyn std::error::Error>> {
    match get_stdout_text(
        "git",
        vec!["show", hash, "--name-status", "--pretty=format:"],
    ) {
        Ok(s) => Ok(s
            .split('\n')
            .filter(|x| x.len() > 0)
            .map(|x| x.split('\t').collect())
            .filter(|x: &Vec<&str>| x[0] == "A" || x[0] == "M")
            .map(|x| x[1])
            .map(|x| x.to_string())
            .collect()),
        Err(err) => return Err(err),
    }
}

// fn get_diff(hash: &str) -> Result<String, Box<dyn std::error::Error>> {
//     get_stdout_text("git", vec!["show", hash])
// }

fn main() {
    let mut param = Parameters::new();

    let app = App::new("ggpc")
        .version("0.1.0")
        .author("Niumlaque <niumlaque@gmail.com>")
        .about("Grep Git's Past Commit")
        .arg(
            Arg::with_name("PATTERN")
                .help("Pattern for searching commits")
                .required(true),
        )
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

    let revlist;
    match get_rev_list(param.all) {
        Ok(l) => revlist = l,
        Err(err) => {
            println!("{}", err);
            std::process::exit(1);
        }
    }

    if param.verbose {
        println!("Found {} commits", revlist.len());
    }

    for hash in &revlist {
        println!("======== {} ========", hash);
        match get_files(hash) {
            Ok(files) => println!("{:?}", files),
            Err(err) => println!("{}", err),
        }
    }
}
