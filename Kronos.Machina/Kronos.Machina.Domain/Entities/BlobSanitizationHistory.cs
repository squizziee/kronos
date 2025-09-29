namespace Kronos.Machina.Domain.Entities
{
    public class BlobSanitizationHistory
    {
        private int _nextEntryIndex;
        public ICollection<BlobSanitizationHistoryEntry> Entries { get; set; } = null!;

        public void AddEntry(string description, bool isSuccessful)
        {
            Entries.Add
            (
                new () 
                { 
                    Description = description, 
                    IsSuccessful = isSuccessful,
                    CreatedAt = DateTime.UtcNow,
                    OrderNumber = _nextEntryIndex,
                }
            );

            ++_nextEntryIndex;
        }
    }
}
