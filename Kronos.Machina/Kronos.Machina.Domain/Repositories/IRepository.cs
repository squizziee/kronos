using Kronos.Machina.Domain.Misc;
using Kronos.Machina.Domain.Specifications;

namespace Kronos.Machina.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task GetByIdAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, 
            CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity,
            CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(TEntity entity,
            CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity,
            CancellationToken cancellationToken = default);
        Task<PaginatedEnumerable<TEntity>> SearchAsync(ISpecification<TEntity> specification, 
            CancellationToken cancellationToken = default);
    }
}
