namespace Kronos.Machina.Domain.Entities
{
    public class SanitizationData
    {
        public BlobSanitizationState State { get; set; }
        public int NextStageNumber {  get; set; }
        public BlobSanitizationHistory History { get; set; } = null!;
    }
}
