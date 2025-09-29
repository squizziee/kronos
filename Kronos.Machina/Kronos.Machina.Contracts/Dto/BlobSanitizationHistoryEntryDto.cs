namespace Kronos.Machina.Contracts.Dto
{
    public record BlobSanitizationHistoryEntryDto
    {
        public required int OrderNumber { get; set; }
        public required bool IsSuccessful { get; set; }
        public required string Description { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
    }
}
