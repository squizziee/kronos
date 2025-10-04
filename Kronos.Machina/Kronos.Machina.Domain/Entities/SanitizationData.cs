namespace Kronos.Machina.Domain.Entities
{
    /// <summary>
    /// Contains all data about sanitization of a video 
    /// blob for a particular video.
    /// </summary>
    public class SanitizationData
    {
        public BlobSanitizationState State { get; set; }
        /// <summary>
        /// Used by sanitization orchestrator and is redundant after
        /// sanitization cycle end.
        /// </summary>
        public int NextStageNumber {  get; set; }
        public BlobSanitizationHistory History { get; set; } = null!;
    }
}
