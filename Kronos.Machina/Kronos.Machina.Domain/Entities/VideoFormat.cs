namespace Kronos.Machina.Domain.Entities
{
    // TODO
    // Add repository for this entity
    public class VideoFormat : BaseEntity
    {
        public required string Name { get; set; }
        public required byte[] Signature { get; set; }
        public required byte[] Extension { get; set; }
    }
}
