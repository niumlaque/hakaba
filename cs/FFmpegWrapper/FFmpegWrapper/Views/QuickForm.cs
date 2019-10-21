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
    }
}
