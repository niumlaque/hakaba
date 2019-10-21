namespace FFmpegWrapper.Models
{
    class CommandBuilder
    {
        private readonly string ffmpegPath;
        private readonly string source;
        private readonly string destination;

        public CommandBuilder(string ffmpegPath, string source, string destination)
        {
            this.ffmpegPath = ffmpegPath;
            this.source = source;
            this.destination = destination;
        }

        public string Build()
        {
            return string.Format("{0} -i {1} {2}", this.ffmpegPath, source, destination);
        }
    }
}
