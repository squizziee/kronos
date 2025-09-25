namespace Kronos.Machina.Domain.Entities
{
    public class BlobSanitizationHistoryEntry
    {
        public int OrderNumber { get; set; }
        public string StageType { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
    }
}
