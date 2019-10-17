namespace FFmpegWrapper
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Initialize())
            {
                return;
            }

            if (args.Length > 0)
            {
                var file = args[0];
                if (!File.Exists(file))
                {
                    App.Error("{0} が見つかりませんでした。", file);
                    return;
                }

                using (var form = new Views.QuickForm())
                {
                    form.Text = string.Format("{0} - クイック", App.Caption);
                    Application.Run(form);
                }
            }
            else
            {
                using (var form = new Views.MainForm())
                {
                    form.Text = App.Caption;
                    Application.Run(form);
                }
            }
        }

        private static bool Initialize()
        {

            App.Caption = "FFmpeg Wrapper";

            try
            {
                App.Load();
            }
            catch (Exception ex)
            {
                App.Error(ex.Message);
            }

            if (App.Settings == null)
            {
                if (App.Question("設定ファイルを読み込めませんでした。\r\n新しく作成します。よろしいですか？") == DialogResult.OK)
                {
                    App.InitializeSettings();
                    App.Save();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
