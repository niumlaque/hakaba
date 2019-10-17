namespace FFmpegWrapper.Views
{
    using System;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
        }

        private void OnOptionsToolStripMenuItemClicked(object sender, EventArgs e)
        {
            using (var dialog = new OptionsForm())
            {
                dialog.Text = string.Format("{0} - オプション", App.Caption);
                dialog.ShowDialog();
            }
        }

        private void OnDownloadToolStripMenuItemClicked(object sender, EventArgs e)
        {
            using (var dialog = new DownloadForm())
            {
                dialog.Text = string.Format("{0} - ダウンロード", App.Caption);
                dialog.ShowDialog();
            }
        }

        private void OnExitToolStripMenuItemClicked(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
