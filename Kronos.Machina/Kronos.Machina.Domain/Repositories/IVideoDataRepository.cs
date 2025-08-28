using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Domain.Repositories
{
    public interface IVideoDataRepository : IRepository<VideoData>
    {
        Task<VideoData> GetVideoDataByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddVideoDataAsync(VideoData data, CancellationToken cancellationToken = default);
        Task DeleteVideoDataAsync(VideoData data, CancellationToken cancellationToken = default);
        Task SoftDeleteVideoDataAsync(VideoData data, CancellationToken cancellationToken = default);
        Task UpdateVideoDataAsync(VideoData data, CancellationToken cancellationToken = default);
        Task UpdateVideoUploadStateAsync(VideoData data, CancellationToken cancellationToken = default);
        Task SetBlobIdAsync(VideoData data, Guid blobId, CancellationToken cancellationToken = default);
        Task AddQualityAsync(VideoData data, VideoImageQuality quality,
            CancellationToken cancellationToken = default);
    }
}
