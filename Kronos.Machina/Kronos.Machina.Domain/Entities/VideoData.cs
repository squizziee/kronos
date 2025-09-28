namespace Kronos.Machina.Domain.Entities
{
    /// <summary>
    /// Stores all info about the video file.
    /// </summary>
    public class VideoData : BaseEntity
    {
        //public required Guid VideoId { get; set; }
        public VideoUploadData UploadData { get; set; } = null!;
        public VideoOrientation Orientation { get; set; }
        public Guid? VideoFormatId { get; set; }
        public VideoFormat? VideoFormat { get; set; }
        public ICollection<VideoImageQuality> AvailableImageQuality { get; set; } = null!;

    }
}
