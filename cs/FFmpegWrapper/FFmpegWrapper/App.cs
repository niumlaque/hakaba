﻿namespace FFmpegWrapper
{
    using System.Windows.Forms;
    using FFmpegWrapper.Models;

    internal class App
    {
        public static string Caption { get; set; }
        public static Settings Settings { get; private set; } = new Settings();

        public static void InitializeSettings()
        {
            Settings = new Settings();
        }

        public static void Load()
        {
            Settings = Settings.Load();
        }

        public static void Save()
        {
            Settings.Save();
        }

        public static DialogResult Error(string format, params object[] args)
        {
            return MessageBox.Show(string.Format(format,args), Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Info(string format, params object[] args)
        {
            return MessageBox.Show(string.Format(format, args), Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Question(string format, params object[] args)
        {
            return MessageBox.Show(string.Format(format, args), Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }
    }
}
