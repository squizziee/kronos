namespace Kronos.Machina.Domain.Entities
{
    /// <summary>
    /// Base for all non-owned entities. Conatins an identifier, 
    /// two timestamps and soft deletion support.
    /// </summary>
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public bool IsSoftDeleted { get; set; }
    }
}
