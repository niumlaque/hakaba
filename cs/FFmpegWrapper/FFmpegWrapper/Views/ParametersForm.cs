namespace FFmpegWrapper.Views
{
    using System.IO;
    using System.Windows.Forms;

    public partial class ParametersForm : Form
    {
        public ParametersForm()
        {
            this.InitializeComponent();
            this.Initialize();
        }

        private void Initialize()
        {
            // ビデオエンコード
            new Behaviours.ComboBoxItems(this.cmbVideoCodec,
                ("-c:v rawvideo", "無劣化 avi 出力"),
                ("-c:v libx264", "x264"),
                ("-c:v libx265", "x265"),
                ("-c:v h264_qsv", "H.264(QSV)"),
                ("-c:v hevc_nvenc", "H.265(NVEnc)"));
        }

        private void OnUseBFrameCheckedChanged(object sender, System.EventArgs e)
        {
            this.gBoxBFrame.Enabled = this.chkUseBFrame.Checked;
        }

        private void OnSourceRefButtonClicked(object sender, System.EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (File.Exists(this.txtSource.Text))
                {
                    dialog.InitialDirectory = Path.GetDirectoryName(this.txtSource.Text);
                }

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtSource.Text = dialog.FileName;
                }
            }
        }

        private void OnDestRefButtonClicked(object sender, System.EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtDestination.Text = dialog.FileName;
                }
            }
        }
    }
}
