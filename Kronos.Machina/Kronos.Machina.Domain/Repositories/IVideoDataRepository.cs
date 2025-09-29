using Kronos.Machina.Domain.Entities;

namespace Kronos.Machina.Domain.Repositories
{
    /// <summary>
    /// Repository interface made to manage <see cref="VideoData"/> and all
    /// the entities that it owns.
    /// </summary>
    public interface IVideoDataRepository : IRepository<VideoData>
    {
        Task<IEnumerable<VideoData>> GetAllAsync(CancellationToken cancellationToken);

        Task UpdateUploadStateByIdAsync(Guid id, VideoUploadState newState,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Note: this method should preferrably update <see cref="SanitizationData.History"/>
        /// after the state update transaction is confirmed.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="newState"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateBlobSanitizationStateByIdAsync(Guid id, BlobSanitizationState newState,
            CancellationToken cancellationToken = default);

        Task SetBlobIdByIdAsync(Guid videoDataId, Guid blobId, 
            CancellationToken cancellationToken = default);

        Task AddQualityByIdAsync(Guid id, VideoImageQuality quality,
            CancellationToken cancellationToken = default);

        void AttachModified(VideoData videoData);
    }
}
