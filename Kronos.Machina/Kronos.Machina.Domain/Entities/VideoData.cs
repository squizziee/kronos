namespace Kronos.Machina.Domain.Entities
{
    public class VideoData : BaseEntity
    {
        public required VideoUploadData UploadData { get; set; }
        public required VideoOrientation Orientation { get; set; }
        public required Guid VideoFormatId { get; set; }
        public required VideoFormat VideoFormat { get; set; }
        public required ICollection<VideoImageQuality> AvailableImageQuality { get; set; }

    }
}
