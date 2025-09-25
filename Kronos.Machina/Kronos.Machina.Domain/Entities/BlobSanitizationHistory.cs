namespace Kronos.Machina.Domain.Entities
{
    public class BlobSanitizationHistory
    {
        public ICollection<BlobSanitizationHistoryEntry> Entries { get; set; } = null!;
    }
}
