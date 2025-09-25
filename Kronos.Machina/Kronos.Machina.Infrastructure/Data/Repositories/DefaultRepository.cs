using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Domain.Misc;
using Kronos.Machina.Domain.Repositories;
using Kronos.Machina.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Kronos.Machina.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Default implementation for a generic repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class DefaultRepository<TEntity> : 
        IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly VideoDatabaseContext _context;

        protected DefaultRepository(VideoDatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(entity, cancellationToken);
        }

        public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Remove(entity);

            return Task.CompletedTask;
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.FindAsync<TEntity>(id, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _context.VideoData.Where(_ => true).Include(vd => vd.AvailableImageQuality).AsSplitQuery();
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<PaginatedEnumerable<TEntity>> SearchAsync(ISpecification<TEntity> specification, 
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Entry(entity).Entity.IsSoftDeleted = true;

            return Task.CompletedTask;
        }

        public abstract Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
