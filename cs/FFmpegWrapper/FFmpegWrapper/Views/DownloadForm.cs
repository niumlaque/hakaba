namespace FFmpegWrapper.Views
{
    using System.Diagnostics;
    using System.Windows.Forms;
    public partial class DownloadForm : Form
    {
        public DownloadForm()
        {
            this.InitializeComponent();
            this.Text = string.Format("{0} - ダウンロード", App.Caption);
        }

        private void OnFMLinkLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var uri = this.lblFM.Text;
            Process.Start(uri);
        }
    }
}
