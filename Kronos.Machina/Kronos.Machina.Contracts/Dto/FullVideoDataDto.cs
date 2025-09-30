namespace Kronos.Machina.Contracts.Dto
{
    public record FullVideoDataDto
    {
        public required VideoUploadDataDto UploadData { get; set; }
        public required string Orientation { get; set; }
        public Guid? VideoFormatId { get; set; }
        public VideoFormatDto? VideoFormat { get; set; }
        public required ICollection<string> AvailableImageQuality { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public required DateTimeOffset LastUpdatedAt { get; set; }
    }
}
