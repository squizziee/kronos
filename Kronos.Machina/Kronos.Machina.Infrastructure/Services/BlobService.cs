using Kronos.Machina.Application.Services;
using Kronos.Machina.Infrastructure.Data.BlobStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Kronos.Machina.Infrastructure.Services
{
    public class BlobService : IBlobService
    {
        private readonly IBlobStorage _blobStorage;
        private readonly ILogger<BlobService> _logger;

        public BlobService(IBlobStorage blobStorage,
            ILogger<BlobService> logger)
        {
            _blobStorage = blobStorage;
            _logger = logger;
        }

        public async Task<string?> SaveToBlob(IFormFile source, CancellationToken cancellationToken = default)
        {
            IBlobIdentifier? blobIdentifier;

            try
            {
                blobIdentifier = await _blobStorage.AddBlobToStorageAsync(source, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to save new blob: {err}", ex.Message);

                return null;
            }

            _logger.LogInformation("New blob saved, identifier: {id}", blobIdentifier.ToString());

            return blobIdentifier.GetNormalizedName();
        }
    }
}
