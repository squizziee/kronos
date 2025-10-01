namespace Kronos.Machina.Infrastructure.ConfigOptions
{
    public record FFmpegConfig
    {
        public required string ProbePath { get; set; }
        public required string EncoderPath { get; set; }
    }
}
