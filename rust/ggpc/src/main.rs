use std::fmt;
use std::process::Command;
use std::str;
extern crate clap;
use clap::{App, Arg};

#[derive(Debug)]
struct Parameters {
    pattern: String,
    verbose: bool,
    all: bool,
}

impl Parameters {
    fn new() -> Self {
        Self {
            pattern: "".to_string(),
            verbose: false,
            all: false,
        }
    }
}

impl fmt::Display for Parameters {
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
        write!(
            f,
            "pattern={}, verbose={}, all={}",
            self.pattern, self.verbose, self.all
        )
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
            .map(|x| x[1].to_string())
            .collect()),
        Err(err) => Err(err),
    }
}

fn get_file_text(hash: &str, file: &str) -> Result<Vec<String>, Box<dyn std::error::Error>> {
    // TODO:　ここで Utf8Error が出る。 多分 bin ファイルを読み込んだとき。
    //        無視して次の検索をかけたい
    match get_stdout_text("git", vec!["show", &format!("{}:{}", hash, file)]) {
        Ok(s) => Ok(s.split('\n').map(|x| x.to_string()).collect()),
        Err(err) => Err(err),
    }
}

fn grep_text(text: &Vec<String>, pattern: &str) -> Option<Vec<(usize, String)>> {
    let mut result: Vec<(usize, String)> = vec![];
    for i in 0..text.len() {
        let line: String = text[i].to_string();
        // 該当箇所の色付けとかしてみたい。
        // indexof って無い？
        if line.contains(pattern) {
            result.push((i + 1, line));
        }
    }

    match result.len() {
        0 => None,
        _ => Some(result),
    }
}

fn main() -> Result<(), Box<dyn std::error::Error>> {
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
    if let Some(pattern) = matches.value_of("PATTERN") {
        param.pattern = pattern.to_string();
    }

    param.verbose = matches.is_present("verbose");
    param.all = matches.is_present("all");
    let param = param; // これでこれ以降 param が変更できなくなる

    if param.verbose {
        println!("{}", param);
    }

    // ? をつけると Error だった場合はその場で return する
    let revlist = get_rev_list(param.all)?;
    if param.verbose {
        println!("Found {} commits", revlist.len());
    }

    for hash in &revlist {
        let files = get_files(hash)?;
        for file in &files {
            let text = get_file_text(hash, file)?;
            if let Some(ls) = grep_text(&text, &param.pattern) {
                for v in ls {
                    println!("{} {}({}): {}", hash, file, v.0, v.1);
                }
            }
        }
    }

    Ok(())
}
