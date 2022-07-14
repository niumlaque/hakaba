use anyhow::Result;
use clap::{Parser, Subcommand};
use image::io::Reader as ImageReader;
use image::Rgb;
pub mod color;

#[derive(Parser, Debug)]
#[clap(author, version, about, long_about = None)]
struct Cli {
    #[clap(subcommand)]
    command: Commands,
}

#[derive(Subcommand, Debug)]
enum Commands {
    //     Grayscale {
    #[clap(long_about = "Grayscale")]
    Gs {
        #[clap(short, value_parser)]
        div: u8,
        #[clap(short, long, value_parser)]
        input: String,
        #[clap(short, long, value_parser)]
        output: Option<String>,
    },
    //     UniformQuantization {
    #[clap(long_about = "Uniform quantization")]
    Uq {
        #[clap(short, value_parser)]
        r: u8,
        #[clap(short, value_parser)]
        g: u8,
        #[clap(short, value_parser)]
        b: u8,
        #[clap(short, long, value_parser)]
        input: String,
        #[clap(short, long, value_parser)]
        output: Option<String>,
    },
}

fn main() -> Result<()> {
    let cli = Cli::parse();
    match cli.command {
        Commands::Gs { div, input, output } => {
            let output = if output == None {
                Some(format!("{}.out.png", input))
            } else {
                output
            };

            let img = ImageReader::open(&input)?.decode()?;
            let src = img.as_rgb8().unwrap();
            let dim = src.dimensions();
            let mut dest = image::RgbImage::new(dim.0, dim.1);
            let gs = color::Grayscale::new(div);
            for ((_, _, sp), (_, _, dp)) in src.enumerate_pixels().zip(dest.enumerate_pixels_mut())
            {
                let c = gs.get(sp);
                *dp = c;
            }

            dest.save(&output.unwrap())?;
        }
        Commands::Uq {
            r,
            g,
            b,
            input,
            output,
        } => {
            let output = if output == None {
                Some(format!("{}.out.png", input))
            } else {
                output
            };
            let img = ImageReader::open(&input)?.decode()?;
            let src = img.as_rgb8().unwrap();
            let dim = src.dimensions();
            let mut dest = image::RgbImage::new(dim.0, dim.1);
            let uq = color::UniformQuantization::new(Rgb([r, g, b]));
            for ((_, _, sp), (_, _, dp)) in src.enumerate_pixels().zip(dest.enumerate_pixels_mut())
            {
                let c = uq.get(sp);
                *dp = c;
            }

            dest.save(&output.unwrap())?;
        }
    }

    Ok(())
}
