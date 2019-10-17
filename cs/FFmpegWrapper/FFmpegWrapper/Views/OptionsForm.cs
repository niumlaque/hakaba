namespace FFmpegWrapper.Views
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            this.InitializeComponent();
            this.txtFFmpegPath.Text = App.Settings.FFmpegPath;
            this.txtFFprobePath.Text = App.Settings.FFprobePath;
        }

        private void OnFFmpegPathButtonClicked(object sender, System.EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (!string.IsNullOrWhiteSpace(this.txtFFmpegPath.Text))
                {
                    dialog.InitialDirectory = Path.GetDirectoryName(this.txtFFmpegPath.Text);
                }

                if (!Directory.Exists(dialog.InitialDirectory))
                {
                    dialog.InitialDirectory = Application.StartupPath;
                }

                dialog.Multiselect = false;
                dialog.Filter = "FFmpeg executable(ffmpeg.exe)|ffmpeg.exe|All Files(*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtFFmpegPath.Text = dialog.FileName;
                }
            }
        }

        private void OnFFprobePathButtonClicked(object sender, System.EventArgs e)
        {
            var initialDirectory = Application.StartupPath;
            if (File.Exists(this.txtFFprobePath.Text))
            {
                initialDirectory = Path.GetDirectoryName(this.txtFFprobePath.Text);
            }
            else if (File.Exists(this.txtFFmpegPath.Text))
            {
                initialDirectory = Path.GetDirectoryName(this.txtFFmpegPath.Text);
            }

            using (var dialog = new OpenFileDialog())
            {
                if (!string.IsNullOrWhiteSpace(this.txtFFprobePath.Text))
                {
                    dialog.InitialDirectory = initialDirectory;
                }

                if (!Directory.Exists(dialog.InitialDirectory))
                {
                    dialog.InitialDirectory = initialDirectory;
                }

                dialog.Multiselect = false;
                dialog.Filter = "FFprobe executable(ffprobe.exe)|ffprobe.exe|All Files(*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtFFprobePath.Text = dialog.FileName;
                }
            }
        }

        private void OKButtonClicked(object sender, System.EventArgs e)
        {
            if (!File.Exists(this.txtFFmpegPath.Text))
            {
                App.Error("FFmpeg が見つかりませんでした。");
                this.txtFFmpegPath.Focus();
                this.txtFFmpegPath.SelectAll();
                return;
            }

            if (!File.Exists(this.txtFFprobePath.Text))
            {
                App.Error("FFprobe が見つかりませんでした。");
                this.txtFFprobePath.Focus();
                this.txtFFprobePath.SelectAll();
                return;
            }

            App.Settings.FFmpegPath = this.txtFFmpegPath.Text;
            App.Settings.FFprobePath = this.txtFFprobePath.Text;

            try
            {
                App.Save();
            }
            catch (Exception ex)
            {
                App.Error("保存に失敗しました。{0}{1}", Environment.NewLine, ex.Message);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OnCancelButtonClicked(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
