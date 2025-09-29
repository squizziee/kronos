namespace Kronos.Machina.Contracts.Dto
{
    public record BlobDataDto
    {
        public required string BlobId { get; set; }
        public SanitizationDataDto SanitizationData { get; set; } = null!;
    }
}
