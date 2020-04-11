extern crate clap;
use clap::{App, Arg};
use lifegame::patterns;
use rand::Rng;

struct LifeGame {
    cells: Vec<Vec<bool>>,
    gen: usize,
}

impl LifeGame {
    fn new(row_size: usize, col_size: usize) -> Self {
        let mut game = Self {
            cells: Vec::with_capacity(row_size),
            gen: 0,
        };

        let mut rng = rand::thread_rng();
        game.cells.resize(row_size, vec![]);
        for row in 0..game.cells.len() {
            game.cells[row].resize(col_size, false);
            for col in 0..game.cells[row].len() {
                game.cells[row][col] = rng.gen();
            }
        }

        return game;
    }

    fn overwrite(&mut self, map: &[u8], row_size: usize, col_size: usize) {
        self.cells.resize(row_size, vec![]);
        for row in 0..self.cells.len() {
            self.cells[row].resize(col_size, false);
            for col in 0..self.cells[row].len() {
                self.cells[row][col] = map[(row * col_size) + col] > 0;
            }
        }
    }

    fn is_alive(&self, row: isize, col: isize) -> bool {
        match self.cells.get(row as usize) {
            Some(r) => match r.get(col as usize) {
                Some(cell) => {
                    if *cell {
                        return true;
                    }
                }
                _ => (),
            },
            _ => (),
        }

        false
    }

    fn check_cell(&self, row: isize, col: isize) -> usize {
        let count: [bool; 8] = [
            self.is_alive(row - 1, col - 1),
            self.is_alive(row - 1, col),
            self.is_alive(row - 1, col + 1),
            self.is_alive(row, col - 1),
            self.is_alive(row, col + 1),
            self.is_alive(row + 1, col - 1),
            self.is_alive(row + 1, col),
            self.is_alive(row + 1, col + 1),
        ];

        return count.iter().filter(|&x| *x).count();
    }

    fn update(&mut self) {
        self.gen = self.gen + 1;
        let mut cloned = self.cells.clone();
        for row in 0..self.cells.len() {
            for col in 0..self.cells[row].len() {
                let val = self.check_cell(row as isize, col as isize);
                match val {
                    2 => cloned[row][col] = cloned[row][col],
                    3 => cloned[row][col] = true,
                    _ => cloned[row][col] = false,
                }
            }
        }

        self.cells = cloned;
    }

    fn output(&self) {
        print!("\x1b[2J");
        println!("Gen {}", self.gen);
        for row in 0..self.cells.len() {
            for col in 0..self.cells[row].len() {
                let ch = if self.cells[row][col] { "██" } else { "[]" };
                print!("{}", ch);
            }
            println!("");
        }
    }
}

fn pulser(game: &mut LifeGame) {
    let map = patterns::get_pulser();
    game.overwrite(&map, 15, 15);
}

fn galaxy(game: &mut LifeGame) {
    let map = patterns::get_galaxy();
    game.overwrite(&map, 15, 15);
}

fn glider(game: &mut LifeGame) {
    let map = patterns::get_glider();
    game.overwrite(&map, 20, 38);
}

fn main() {
    let app = App::new("lifegame")
        .version("0.1.0")
        .arg(
            Arg::with_name("pulser")
                .help("Starts with pulser pattern")
                .long("pulser"),
        )
        .arg(
            Arg::with_name("galaxy")
                .help("Starts with galaxy pattern")
                .long("galaxy"),
        )
        .arg(
            Arg::with_name("glider")
                .help("Starts with glider pattern")
                .long("glider"),
        );

    let matches = app.get_matches();
    let mut game = LifeGame::new(21, 42);
    if matches.is_present("pulser") {
        pulser(&mut game);
    } else if matches.is_present("galaxy") {
        galaxy(&mut game);
    } else if matches.is_present("glider") {
        glider(&mut game);
    }

    loop {
        game.output();
        println!("Press any key to next generation or q to exit");

        let mut input = String::new();
        std::io::stdin().read_line(&mut input).ok();
        if input.trim() == "q" {
            break;
        } else {
            game.update();
        }
    }
}
