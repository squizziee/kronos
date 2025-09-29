namespace Kronos.Machina.Contracts.Dto
{
    public record SanitizationDataDto
    {
        public required string State { get; set; }
        public required BlobSanitizationHistoryDto History { get; set; }
    }
}
