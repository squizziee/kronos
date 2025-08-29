namespace Kronos.Machina.Domain.Misc
{
    public record PaginatedEnumerable<TEntity> where TEntity : class
    {
        public required int PageNumber { get; init; }
        public required int TotalPahges { get; init; }
        public required IEnumerable<TEntity> Items { get; init; }
    }
}
