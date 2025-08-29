using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Domain.Repositories
{
    public interface IVideoFormatRepository : IRepository<VideoFormat>
    {
        Task<VideoFormat> GetByNameAsync(string name, 
            CancellationToken cancellationToken = default);
        Task<IEnumerable<VideoFormat>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
