package main

import (
	"fmt"
	"os"
	"path/filepath"

	"github.com/jessevdk/go-flags"
	. "github.com/kkdai/youtube"
)

// アプリケーション本体のオプション
type options struct {
	Download DownloadOptions `description:"Downlaod video" command:"download"`
	Info     InfoOptions     `description:"Display video information" command:"info"`
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

// for go-flag
// download コマンドが指定された際に実行される
func (do *DownloadOptions) Execute(args []string) error {
	fmt.Println("download")
	err := download(do.Dir, do.Filename, do.VideoId, do.Quality, do.ItagNo, false)
	return err
}

// info コマンドが指定された際に実行される
func (io *InfoOptions) Execute(args []string) error {
	err := printVideoInfo(io.VideoId, false)
	return err
}

// ダウンロード処理
func download(dir, filename, videoId, quality string, itagNo int, verbose bool) error {
	y := NewYoutube(verbose)
	if err := y.DecodeURL(videoId); err != nil {
		return err
	}

	tempFilename := "." + videoId + ".mp4"

	if err := y.StartDownload(dir, tempFilename, quality, itagNo); err != nil {
		return err
	}

	source := filepath.Join(dir, tempFilename)
	dest := filepath.Join(dir, filename)

	if err := os.Rename(source, dest); err != nil {
		return err
	}

	return nil
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

func main() {
	opts := &options{}
	parser := flags.NewParser(opts, flags.HelpFlag)
	if _, err := parser.Parse(); err != nil {
		fmt.Fprintf(os.Stderr, "%s\n", err.Error())
		os.Exit(1)
	}
}
