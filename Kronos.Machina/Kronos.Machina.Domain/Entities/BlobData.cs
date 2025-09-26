namespace Kronos.Machina.Domain.Entities
{
    public class BlobData
    {
        public string BlobId { get; set; } = string.Empty;
        public SanitizationData SanitizationData { get; set; } = null!;
    }
}
