using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Domain.Repositories
{
    public interface IVideoFormatRepository : IRepository<VideoFormat>
    {
        Task<VideoFormat> GetVideoFormatByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<VideoFormat> GetVideoFormatByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<VideoFormat>> GetAllVideoFormatsAsync(CancellationToken cancellationToken = default);
        Task AddVideoFormatAsync(VideoFormat videoFormat, CancellationToken cancellationToken = default);
        Task UpdateVideoFormatAsync(VideoFormat videoFormat, CancellationToken cancellationToken = default);
        Task DeleteVideoFormatAsync(VideoFormat videoFormat, CancellationToken cancellationToken = default);
        Task SoftDeleteVideoFormatAsync(VideoFormat videoFormat, CancellationToken cancellationToken = default);
    }
}
