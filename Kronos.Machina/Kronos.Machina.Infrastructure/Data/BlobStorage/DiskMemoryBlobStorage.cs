using Kronos.Machina.Infrastructure.ConfigOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kronos.Machina.Infrastructure.Data.BlobStorage
{
    public class DiskMemoryBlobStorage : IBlobStorage
    {
        private readonly SanitizedBlobZoneInfo _sanitizedBlobZoneInfo;
        private readonly UnsanitizedBlobZoneInfo _unsanitizedBlobZoneInfo;
        private readonly ILogger<DiskMemoryBlobStorage> _logger;

        public DiskMemoryBlobStorage(IOptionsSnapshot<SanitizedBlobZoneInfo> options1,
            IOptionsSnapshot<UnsanitizedBlobZoneInfo> options2,
            ILogger<DiskMemoryBlobStorage> logger)
        {
            _sanitizedBlobZoneInfo = options1.Value;
            _unsanitizedBlobZoneInfo = options2.Value;
            _logger = logger;
        }

        public async Task<IBlobIdentifier> AddBlobToStorageAsync(Stream blobData, 
            CancellationToken cancellationToken = default)
        {
            var blobId = DiskMemoryBlobIdentifier.NewIdentifier();

            return await AddBlobToStorageInternalAsync(blobId, blobData, cancellationToken);
        }

        public async Task<IBlobIdentifier> AddBlobToStorageAsync(IFormFile blobData,
            CancellationToken cancellationToken = default)
        {
            var blobId = DiskMemoryBlobIdentifier.NewIdentifier();

            return await AddBlobToStorageInternalAsync(blobId, blobData, cancellationToken);
        }

        private async Task<IBlobIdentifier> AddBlobToStorageInternalAsync(IBlobIdentifier blobId, 
            Stream blobData, CancellationToken cancellationToken = default)
        {
            try
            {
                using var file = File.OpenWrite(
                    $"{_unsanitizedBlobZoneInfo.BlobPath}/{blobId.GetStorageName()}"
                );

                await blobData.CopyToAsync(file, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to add blob to disk storage: {err}", ex.Message);
            }

            _logger.LogInformation("New blob added to disk quarantine zone: {id}", blobId.GetStorageName());

            return blobId;
        }

        private async Task<IBlobIdentifier> AddBlobToStorageInternalAsync(IBlobIdentifier blobId,
            IFormFile blobData, CancellationToken cancellationToken = default)
        {
            try
            {
                using var file = File.OpenWrite(
                    $"{_unsanitizedBlobZoneInfo.BlobPath}/{blobId.GetStorageName()}"
                );

                await blobData.CopyToAsync(file, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to add blob to disk storage: {err}", ex.Message);
            }

            _logger.LogInformation("New blob added to disk quarantine zone: {id}", blobId.GetStorageName());

            return blobId;
        }

        public Task ChangeBlobIdentifierAsync(IBlobIdentifier oldIdentifier, 
            IBlobIdentifier newIdentifier, CancellationToken cancellationToken = default)
        {
            var oldPath = $"{_unsanitizedBlobZoneInfo.BlobPath}/{oldIdentifier.GetStorageName()}";
            var newPath = $"{_unsanitizedBlobZoneInfo.BlobPath}/{oldIdentifier.GetStorageName()}";
            File.Move(oldPath, newPath);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<Stream> GetBlobDataAsync(IBlobIdentifier blobIdentifier, 
            CancellationToken cancellationToken = default)
        {
            var path = $"{_unsanitizedBlobZoneInfo.BlobPath}/{blobIdentifier.GetStorageName()}";
            using var file = File.OpenRead(path);

            var stream = new MemoryStream();
            await file.CopyToAsync(stream, cancellationToken);

            return stream;
        }

        public async Task<byte[]> GetBlobDataAsync(IBlobIdentifier blobIdentifier, 
            int start, int count, CancellationToken cancellationToken = default)
        {
            var path = $"{_unsanitizedBlobZoneInfo.BlobPath}/{blobIdentifier.GetStorageName()}";
            using var file = File.OpenRead(path);

            var buffer = new byte[count];
            await file.ReadExactlyAsync(buffer, start, count, cancellationToken);

            return buffer;
        }

        public async Task MoveFromQuarantineAsync(IBlobIdentifier blobIdentifier, 
            CancellationToken cancellationToken = default)
        {
            var sourcePath = $"{_unsanitizedBlobZoneInfo.BlobPath}/{blobIdentifier.GetStorageName()}";
            using var sourceFile = File.OpenRead(sourcePath);

            var destinationPath = $"{_sanitizedBlobZoneInfo.BlobPath}/{blobIdentifier.GetStorageName()}";
            using var destinationFile = File.OpenWrite(destinationPath);

            await sourceFile.CopyToAsync(destinationFile, cancellationToken);

            File.Delete(sourcePath);
        }

        public Task RemoveBlobFromStorageAsync(IBlobIdentifier blobIdentifier, 
            CancellationToken cancellationToken = default)
        {
            var path = $"{_unsanitizedBlobZoneInfo.BlobPath}/{blobIdentifier.GetStorageName()}";
            File.Delete(path);

            return Task.CompletedTask;
        }

        public async Task UpdateBlobAsync(IBlobIdentifier blobIdentifier, Stream newBlobData, 
            CancellationToken cancellationToken = default)
        {
            await RemoveBlobFromStorageAsync(blobIdentifier, cancellationToken);
            await AddBlobToStorageInternalAsync(blobIdentifier, newBlobData, cancellationToken);
        }

        public async Task UpdateBlobAsync(IBlobIdentifier blobIdentifier, IFormFile newBlobData, 
            CancellationToken cancellationToken = default)
        {
            await RemoveBlobFromStorageAsync(blobIdentifier, cancellationToken);
            await AddBlobToStorageInternalAsync(blobIdentifier, newBlobData, cancellationToken);
        }
    }
}
