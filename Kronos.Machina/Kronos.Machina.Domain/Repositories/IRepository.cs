using Kronos.Machina.Domain.Misc;
using Kronos.Machina.Domain.Specifications;

namespace Kronos.Machina.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<TEntity?> GetByIdAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, 
            CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity,
            CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(TEntity entity,
            CancellationToken cancellationToken = default);
        /// <summary>
        /// Update method that should be used with detached entities. EF Core
        /// change tracking makes it redundant except the case when you want
        /// to guard some of the properties inside your entity on update.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity,
            CancellationToken cancellationToken = default);
        Task<PaginatedEnumerable<TEntity>> SearchAsync(ISpecification<TEntity> specification, 
            CancellationToken cancellationToken = default);
    }
}
