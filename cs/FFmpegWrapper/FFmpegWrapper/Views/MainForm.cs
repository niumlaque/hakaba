namespace FFmpegWrapper.Views
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using NilaLib;

    public partial class MainForm : Form
    {
        private string fileName;

        public MainForm()
        {
            this.InitializeComponent();
            this.WithParametersForm();
        }

        private string FileName
        {
            get
            {
                return this.fileName;
            }

            set
            {
                value = value ?? string.Empty;
                if (this.fileName != value)
                {
                    this.fileName = value;
                    this.Text = string.Format("{0}({1})", App.Caption, value);
                }
            }
        }

        private void WithParametersForm()
        {
            // 将来的には画面を分けたい
            Views.ParametersForm param = new ParametersForm();
            param.TopLevel = false;
            param.FormBorderStyle = FormBorderStyle.None;
            param.Dock = DockStyle.Fill;
            param.Visible = true;
            this.openToolStripMenuItem.Enabled = false;
            this.convertToolStripMenuItem.Enabled = false;
            this.pnlContent.Controls.Add(param);
        }

        private void OnOptionsToolStripMenuItemClicked(object sender, EventArgs e)
        {
            using (var dialog = new OptionsForm())
            {
                dialog.ShowDialog();
            }
        }

        private void OnDownloadToolStripMenuItemClicked(object sender, EventArgs e)
        {
            using (var dialog = new DownloadForm())
            {
                dialog.ShowDialog();
            }
        }

        private void OnConvertToolStripMenuItemClicked(object sender, EventArgs e)
        {
            if (!File.Exists(App.Settings.FFmpegPath))
            {
                App.Error("ffmpeg のパスを指定してください。");
                return;
            }

            if (!File.Exists(this.FileName))
            {
                App.Error("変換元のファイルを指定してください。");
                return;
            }

            using (var saveDialog = new SaveFileDialog())
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var builder = new Models.CommandBuilder(App.Settings.FFmpegPath, this.fileName, saveDialog.FileName);
                    using (var confirmDialog = new ConfirmationForm(builder.Build()))
                    {
                        confirmDialog.ShowDialog();
                    }
                }
            }
        }

        private void OnOpenToolStripMenuItemClicked(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.FileName = dialog.FileName;
                }
            }
        }

        private void OnExitToolStripMenuItemClicked(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
