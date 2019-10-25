namespace FFmpegWrapper.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class CommandBuilder
    {
        private readonly string ffmpegPath;
        private readonly List<CommandParameter> parameters = new List<CommandParameter>();

        public CommandBuilder(string ffmpegPath, string source, string destination)
        {
            this.ffmpegPath = ffmpegPath;
            this.parameters.Add(new InputFile(source));
            this.parameters.Add(new OutputFile(destination));
        }

        public void Add(CommandParameter param)
        {
            this.parameters.Add(param);
        }

        public string Build()
        {
            var buffer = new StringBuilder(1024);
            buffer.AppendFormat("{0}", this.ffmpegPath);
            foreach (var item in this.parameters.OrderBy(x => x.Priority))
            {
                buffer.AppendFormat(" {0}", item.ToString());
            }

            return buffer.ToString();
        }
    }

    internal abstract class CommandParameter
    {
        public CommandParameter(string v)
        {
            this.Value = v;
        }

        public virtual int Priority { get { return 0; } }
        public virtual bool EqulasRequired { get { return false; } }
        public abstract string Key { get; }
        public virtual string Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}", this.Key ?? string.Empty, this.EqulasRequired ? "=" : " ", this.Value ?? string.Empty);
        }
    }

    internal class InputFile : CommandParameter
    {
        public InputFile(string v) : base(v)
        {
        }

        public override string Key => "-i";
        public override string Value
        {
            get => string.Format("\"{0}\"", base.Value);
            set => base.Value = value;
        }
    }

    internal class OutputFile : CommandParameter
    {
        public OutputFile(string v) : base(v)
        {
        }

        public override int Priority => 100;
        public override string Key => string.Empty;
        public override string Value
        {
            get => string.Format("\"{0}\"", base.Value);
            set => base.Value = value;
        }

        public override string ToString()
        {
            return this.Value;
        }
    }

    internal class VideoCodec : CommandParameter
    {
        public VideoCodec(string v) : base(v)
        {
        }

        public override string Key => "-c:v";
    }
}
