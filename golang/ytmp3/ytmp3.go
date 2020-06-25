package main

import (
	"fmt"
	"os"
	"os/exec"
	"path/filepath"

	"github.com/jessevdk/go-flags"
	. "github.com/kkdai/youtube"
)

var config *Config

// アプリケーション本体のオプション
type options struct {
	Download DownloadOptions `description:"Downlaod video" command:"download"`
	Info     InfoOptions     `description:"Display video information" command:"info"`
	Convert  ConvertOptions  `description:"Convert mp4 to mp3" command:"convert"`
}

// download コマンド時のオプション
type DownloadOptions struct {
	Dir      string `short:"d" long:"dir" required:"true" description:"A directory for saving a file"`
	Filename string `short:"f" long:"file" required:"true" description:"A filename for saving a file"`
	VideoId  string `long:"id" required:"true" description:"Video ID"`
	Quality  string `short:"q" long:"quality" description:"Video quality"`
	ItagNo   int    `short:"i" long:"itag" description:"Video itag"`
}

// info コマンド時のオプション
type InfoOptions struct {
	VideoId string `long:"id" required:"true" description:"Video ID"`
}

// 変換時のオプション
type ConvertOptions struct {
	Source string `short:"f" long:"file" required:"true" description:"Path to source file"`
	Dest   string `short:"o" long:"output" required:"true" description:"Path to output"`
}

// for go-flag
// download コマンドが指定された際に実行される
func (do *DownloadOptions) Execute(args []string) error {
	fmt.Println("download")
	err := download(do.Dir, do.VideoId, do.Quality, do.ItagNo, false)
	if err != nil {
		return err
	}

	err = convertToMP3(config.FFmpegPath, "."+do.VideoId+".mp4", do.Filename, "256k")
	return err
}

// info コマンドが指定された際に実行される
func (io *InfoOptions) Execute(args []string) error {
	err := printVideoInfo(io.VideoId, false)
	return err
}

func (co *ConvertOptions) Execute(args []string) error {
	err := convertToMP3(config.FFmpegPath, co.Source, co.Dest, "256k")
	return err
}

// ダウンロード処理
func download(dir, videoId, quality string, itagNo int, verbose bool) error {
	y := NewYoutube(verbose)
	if err := y.DecodeURL(videoId); err != nil {
		return err
	}

	tempFilename := "." + videoId + ".mp4.temp"
	cmplFileName := "." + videoId + ".mp4"

	if err := y.StartDownload(dir, tempFilename, quality, itagNo); err != nil {
		return err
	}

	source := filepath.Join(dir, tempFilename)
	dest := filepath.Join(dir, cmplFileName)

	if err := os.Rename(source, dest); err != nil {
		return err
	}

	return nil
}

// ffmpeg -i hoge.mp4 -acodec libmp3lame -ab 256k foo.mp3
func convertToMP3(bin, source, dest, bitrate string) error {
	args := []string{"-i", source, "-acodec", "libmp3lame", "-ab", bitrate, dest}
	err := exec.Command(bin, args...).Run()
	return err
}

// ビデオ情報表示処理
func printVideoInfo(videoId string, verbose bool) error {
	y := NewYoutube(verbose)
	if err := y.DecodeURL(videoId); err != nil {
		return err
	}

	fmt.Println("Quality - ItagNo")
	fmt.Println("----------------")
	for _, x := range y.StreamList {
		fmt.Printf("%s - %v\n", x.Quality, x.ItagNo)
	}

	return nil
}

func run(c *Config, args []string) error {
	opts := &options{}
	parser := flags.NewParser(opts, flags.HelpFlag)
	_, err := parser.ParseArgs(args)
	return err
}

func main() {
	c, err := loadConfig()
	if err != nil {
		fmt.Fprintf(os.Stderr, "%s\n", err.Error())
		os.Exit(1)
	}

	config = c
	if err := run(c, os.Args[1:]); err != nil {
		fmt.Fprintf(os.Stderr, "%s\n", err.Error())
		os.Exit(1)
	}
}
