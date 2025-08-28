namespace Kronos.Machina.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
