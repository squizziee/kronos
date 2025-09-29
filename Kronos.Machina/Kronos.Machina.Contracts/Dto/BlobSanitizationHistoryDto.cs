namespace Kronos.Machina.Contracts.Dto
{
    public record BlobSanitizationHistoryDto
    {
        public required ICollection<BlobSanitizationHistoryEntryDto> Entries { get; set; }
    }
}
