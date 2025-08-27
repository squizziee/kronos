namespace Kronos.Machina.Domain.Entities
{
    public class VideoUploadData
    {
        public required VideoUploadState State { get; set; }
        public VideoUploadStrategy UploadStrategy { get; set; }
        public required Guid UploadStrategyId { get; set; }
        public required BlobData BlobData { get; set; }
    }
}
