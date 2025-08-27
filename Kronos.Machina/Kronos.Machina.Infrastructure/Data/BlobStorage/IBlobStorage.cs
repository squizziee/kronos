using Microsoft.AspNetCore.Http;

namespace Kronos.Machina.Infrastructure.Data.BlobStorage
{
    public interface IBlobStorage : IDisposable
    {
        Task<IBlobIdentifier> AddBlobToStorageAsync(Stream blobData,
            CancellationToken cancellationToken = default);
        Task<IBlobIdentifier> AddBlobToStorageAsync(IFormFile blobData,
            CancellationToken cancellationToken = default);
        Task UpdateBlobAsync(IBlobIdentifier blobIdentifier, Stream newblobData,
            CancellationToken cancellationToken = default);
        Task UpdateBlobAsync(IBlobIdentifier blobIdentifier, IFormFile newblobData,
            CancellationToken cancellationToken = default);
        Task RemoveBlobFromStorageAsync(IBlobIdentifier blobIdentifier,
            CancellationToken cancellationToken = default);
        Task<Stream> GetBlobDataAsync(IBlobIdentifier blobIdentifier,
            CancellationToken cancellationToken = default);
        Task<byte[]> GetBlobDataAsync(IBlobIdentifier blobIdentifier, int start, int count,
            CancellationToken cancellationToken = default);
        Task ChangeBlobIdentifierAsync(IBlobIdentifier oldIdentifier, IBlobIdentifier newIdentifier,
            CancellationToken cancellationToken = default);

    }
}
