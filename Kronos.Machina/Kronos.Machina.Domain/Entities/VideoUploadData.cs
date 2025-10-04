namespace Kronos.Machina.Domain.Entities
{
    /// <summary>
    /// Contains all data about the upload process of
    /// the video file.
    /// </summary>
    public class VideoUploadData
    {
        public VideoUploadState State { get; set; }
        public VideoUploadStrategy? UploadStrategy { get; set; }
        public Guid? UploadStrategyId { get; set; }
        public BlobData BlobData { get; set; } = null!;
    }
}
