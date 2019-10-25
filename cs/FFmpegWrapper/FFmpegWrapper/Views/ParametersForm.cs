namespace FFmpegWrapper.Views
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    public partial class ParametersForm : Form
    {
        public ParametersForm()
        {
            this.InitializeComponent();
            this.Initialize();
        }

        internal (Models.CommandBuilder builder, bool ok) GetBuilder()
        {
            var (ffmpeg, ok) = App.GetFFmpegPath();
            if (!ok)
            {
                return (null, false);
            }

            var builder = new Models.CommandBuilder(ffmpeg, this.txtSource.Text, this.txtDestination.Text);
            if (this.cmbVideoCodec.SelectedIndex > 0)
            {
                builder.Add(new Models.VideoCodec(((KeyValuePair<string, string>)this.cmbVideoCodec.SelectedItem).Key));
            }

            return (builder, true);
        }

        private void Initialize()
        {
            // ビデオエンコード
            new Behaviours.ComboBoxItems(this.cmbVideoCodec,
                ("rawvideo", "無劣化 avi 出力"),
                ("libx264", "x264"),
                ("libx265", "x265"),
                ("h264_qsv", "H.264(QSV)"),
                ("hevc_nvenc", "H.265(NVEnc)"));

            this.cmbVideoCodec.SelectedIndex = 0;
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
