namespace Kronos.Machina.Domain.Entities
{
    /// <summary>
    /// Stores supported video format data, used mostly
    /// for sanitization purposes.
    /// </summary>
    public class VideoFormat : BaseEntity
    {
        public required string Name { get; set; }
        public required byte[] Signature { get; set; }
        public required string Extension { get; set; }
    }
}
