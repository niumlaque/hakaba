use image::Rgb;

pub struct Grayscale {
    conv: Converter,
}

impl Grayscale {
    pub fn new(div: u8) -> Self {
        Self {
            conv: Converter::new(div),
        }
    }

    pub fn get(&self, rgb: &Rgb<u8>) -> Rgb<u8> {
        let v = ((rgb[0] as i16 + rgb[1] as i16 + rgb[2] as i16) / 3) as usize;
        Rgb([
            self.conv.buffer[v],
            self.conv.buffer[v],
            self.conv.buffer[v],
        ])
    }
}

#[derive(Debug)]
pub struct UniformQuantization {
    r_conv: Converter,
    g_conv: Converter,
    b_conv: Converter,
}

impl UniformQuantization {
    pub fn new(divs: Rgb<u8>) -> Self {
        Self {
            r_conv: Converter::new(divs[0]),
            g_conv: Converter::new(divs[1]),
            b_conv: Converter::new(divs[2]),
        }
    }

    pub fn get(&self, rgb: &Rgb<u8>) -> Rgb<u8> {
        Rgb([
            self.r_conv.buffer[rgb[0] as usize],
            self.g_conv.buffer[rgb[1] as usize],
            self.b_conv.buffer[rgb[2] as usize],
        ])
    }
}

#[derive(Debug)]
struct Converter {
    pub buffer: [u8; 256],
}

impl Converter {
    pub fn new(div: u8) -> Self {
        let div = div as i16;
        let freq = 256f32 / div as f32;
        let cap = div + 1;
        let mut range = Vec::with_capacity(cap as usize);
        for i in 0..cap {
            range.push(freq * i as f32);
        }

        let freq = 255f32 / (div - 1) as f32;
        let mut c = Vec::with_capacity(div as usize);
        for i in 0..div {
            c.push((freq * i as f32) as u8);
        }

        let mut ret = Self { buffer: [0; 256] };
        let mut j = 0;
        for i in 0..256 {
            if i as f32 >= range[j + 1] {
                j += 1;
            }

            ret.buffer[i] = c[j];
        }

        ret
    }
}
