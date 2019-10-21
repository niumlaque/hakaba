namespace FFmpegWrapper.Views
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    public partial class ConfirmationForm : Form
    {
        private string command = string.Empty;

        public ConfirmationForm() : this(string.Empty)
        {
        }

        public ConfirmationForm(string command)
        {
            this.InitializeComponent();
            this.Text = string.Format("{0} - 確認", App.Caption);
            this.rtBoxCommand.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            this.rtBoxCommand.LostFocus += this.OnCommandTextBoxLostFocus;
            this.rtBoxCommand.TextChanged += this.OnCommandTextChanged;
            this.Command = command;
        }

        private void OnCommandTextChanged(object sender, EventArgs e)
        {
            this.btnExec.Enabled = this.rtBoxCommand.Text.Length > 0;
        }

        public string Command
        {
            get
            {
                return this.command.Replace("\r\n", " ").Replace("\n", " ");
            }

            private set
            {
                if (value == null)
                {
                    value = string.Empty;
                }

                if (this.command != value)
                {
                    this.command = value;
                    this.rtBoxCommand.Text = this.command;
                    this.btnExec.Enabled = !string.IsNullOrWhiteSpace(value);
                }
            }
        }

        private void OnCommandTextBoxLostFocus(object sender, EventArgs e)
        {
            this.Command = this.rtBoxCommand.Text;
        }

        private void OnExecButtonClicked(object sender, EventArgs e)
        {
            var si = new ProcessStartInfo("cmd", "/k " + this.Command);
            using (var proc = new Process())
            {
                proc.StartInfo = si;
                proc.Start();
            }
        }

        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
