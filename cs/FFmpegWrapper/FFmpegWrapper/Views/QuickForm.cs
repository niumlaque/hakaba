namespace FFmpegWrapper.Views
{
    using System;
    using System.Windows.Forms;

    public partial class QuickForm : Form
    {
        private readonly string fileName;

        public QuickForm() : this(string.Empty)
        {
        }

        public QuickForm(string fileName)
        {
            this.InitializeComponent();
            this.fileName = fileName;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Test();
        }

        private void Test()
        {
            using (var confirmDialog = new ConfirmationForm("127.0.0.1"))
            {
                if (confirmDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var execDialog = new ExecutionForm("ping", confirmDialog.Parameters))
                    {
                        execDialog.ShowDialog();
                    }
                }
            }
        }
    }
}
