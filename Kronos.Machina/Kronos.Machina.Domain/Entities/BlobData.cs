namespace Kronos.Machina.Domain.Entities
{
    /// <summary>
    /// Stores all technical data about the video blob (original 
    /// file uploaded by the video author). Includes video and audio channel
    /// info.
    /// </summary>
    public class BlobData
    {
        public string BlobId { get; set; } = string.Empty;
        public SanitizationData SanitizationData { get; set; } = null!;
        public TimeSpan? Duration { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public double? FramesPerSecond { get; set; }
        public int? VideoBitrate { get; set; }
        public string? VideoCodecName { get; set; } = null!;
        public int? BitDepth { get; set; }
        public string? AudioCodecName { get; set; } = null!;
        public string? AudioChannels { get; set; } = null!;
        public int? AudioSampleRate { get; set; }
        public int? AudioBitrate { get; set; }
    }
}
