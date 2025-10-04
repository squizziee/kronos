namespace Kronos.Machina.Domain.Entities
{
    /// <summary>
    /// History of sanitization for a given blob.
    /// </summary>
    public class BlobSanitizationHistory
    {
        /// <summary>
        /// Indicates which index should next entry have. For more information
        /// see <see cref="BlobSanitizationHistoryEntry.OrderNumber"/>.
        /// </summary>
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
