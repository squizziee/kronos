namespace Kronos.Machina.Domain.Entities
{
    public class VideoUploadData
    {
        public VideoUploadState State { get; set; }
        public VideoUploadStrategy? UploadStrategy { get; set; }
        public Guid? UploadStrategyId { get; set; }
        public BlobData BlobData { get; set; } = null!;
    }
}
