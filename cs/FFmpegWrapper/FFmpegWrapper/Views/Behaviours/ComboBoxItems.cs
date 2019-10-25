namespace FFmpegWrapper.Views.Behaviours
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    internal class ComboBoxItems
    {
        private static readonly KeyValuePair<string, string> Empty = new KeyValuePair<string, string>();

        public ComboBoxItems(ComboBox source, params (string, string)[] args)
        {
            try
            {
                source.SuspendLayout();
                source.Items.Clear();
                source.DisplayMember = nameof(Empty.Value);
                source.ValueMember = nameof(Empty.Key);
                foreach (var item in args)
                {
                    source.Items.Add(new KeyValuePair<string, string>(item.Item1, item.Item2));
                }
            }
            finally
            {
                source.ResumeLayout();
            }
        }
    }
}
