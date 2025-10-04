namespace Kronos.Machina.Domain.Entities
{
    /// <summary>
    /// Entry for history of sanitization for a given blob.
    /// </summary>
    public class BlobSanitizationHistoryEntry
    {
        /// <summary>
        /// Although <see cref="BlobSanitizationHistoryEntry.CreatedAt"/>
        /// can be sufficent in many cases for sorting logs in correct order,
        /// in some cases logs might be added with the same timestamp. In
        /// that case the order is always preserved by order number.
        /// </summary>
        public int OrderNumber { get; set; }
        public bool IsSuccessful { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
    }
}
