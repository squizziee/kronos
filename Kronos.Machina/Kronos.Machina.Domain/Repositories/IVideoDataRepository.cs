using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Domain.Repositories
{
    public interface IVideoDataRepository
    {
        Task GetVideoDataByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddVideoDataAsync(VideoData data, CancellationToken cancellationToken);
        Task DeleteVideoDataAsync(VideoData data, CancellationToken cancellationToken);
        Task SoftDeleteVideoDataAsync(VideoData data, CancellationToken cancellationToken);
        Task UpdateVideoDataAsync(VideoData data, CancellationToken cancellationToken);
        Task UpdateVideoUploadStateAsync(VideoData data, CancellationToken cancellationToken);
        Task SetBlobIdAsync(VideoData data, Guid blobId, CancellationToken cancellationToken);
        Task AddQualityAsync(VideoData data, VideoImageQuality quality,
            CancellationToken cancellationToken);
    }
}
