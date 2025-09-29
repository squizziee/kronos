namespace Kronos.Machina.Domain.Entities
{
    public class BlobSanitizationHistoryEntry
    {
        public required int OrderNumber { get; set; }
        public required bool IsSuccessful { get; set; }
        public required string Description { get; set; } = string.Empty;
        public required DateTimeOffset CreatedAt { get; set; }
    }
}
