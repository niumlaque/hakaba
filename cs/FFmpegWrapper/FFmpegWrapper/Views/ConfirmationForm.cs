namespace FFmpegWrapper.Views
{
    using System;
    using System.Windows.Forms;

    public partial class ConfirmationForm : Form
    {
        private string parameters = string.Empty;

        public ConfirmationForm() : this(string.Empty)
        {
        }

        public ConfirmationForm(string parameters)
        {
            this.InitializeComponent();
            this.rtBoxParameters.LostFocus += this.OnParametersTextBoxLostFocus;
            this.rtBoxParameters.TextChanged += OnParametersTextChanged;
            this.Parameters = parameters;
        }

        private void OnParametersTextChanged(object sender, EventArgs e)
        {
            this.btnExec.Enabled = this.rtBoxParameters.Text.Length > 0;
        }

        public string Parameters
        {
            get
            {
                return this.parameters;
            }

            private set
            {
                if (value == null)
                {
                    value = string.Empty;
                }

                if (this.parameters != value)
                {
                    this.parameters = value;
                    this.rtBoxParameters.Text = this.parameters;
                    this.btnExec.Enabled = !string.IsNullOrWhiteSpace(value);
                }
            }
        }

        private void OnParametersTextBoxLostFocus(object sender, EventArgs e)
        {
            this.Parameters = this.rtBoxParameters.Text;
        }

        private void OnExecButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
