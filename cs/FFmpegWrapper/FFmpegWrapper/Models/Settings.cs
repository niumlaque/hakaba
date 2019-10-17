namespace FFmpegWrapper.Models
{
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using System.Windows.Forms;

    public class Settings
    {
        public static readonly string filePath;

        static Settings()
        {
            filePath = Path.Combine(Application.StartupPath, ".FFmpegWrapper.config");
        }

        public Settings()
        {
        }

        public string FFmpegPath { get; set; } = string.Empty;
        public string FFprobePath { get; set; } = string.Empty;

        public static Settings Load()
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(Settings));
            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                return serializer.Deserialize(reader) as Settings;
            }
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(Settings));
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                serializer.Serialize(writer, this);
            }
        }
    }
}
