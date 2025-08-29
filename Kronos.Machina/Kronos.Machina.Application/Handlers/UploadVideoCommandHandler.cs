using Kronos.Machina.Application.Misc.Sanitization;
using Kronos.Machina.Application.Services;
using Kronos.Machina.Contracts.Commands;
using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kronos.Machina.Application.Handlers
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand>
    {
        private readonly IVideoDataRepository _videoDataRepository;
        private readonly IVideoBlobService _videoBlobService;
        private readonly IBlobSanitizationOrchestrator _orchestrator;
        private readonly ILogger<UploadVideoCommandHandler> _logger;


        public UploadVideoCommandHandler(IVideoDataRepository videoDataRepository,
            IVideoBlobService videoBlobService,
            IBlobSanitizationOrchestrator orchestrator,
            ILogger<UploadVideoCommandHandler> logger)
        {
            _videoDataRepository = videoDataRepository;
            _videoBlobService = videoBlobService;
            _orchestrator = orchestrator;
            _logger = logger;
        }

        public async Task Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Blob upload about to start");

            var blobId = await _videoBlobService.SaveToBlob(request.Source);

            _logger.LogDebug("Blob uploaded to quarantine with {id} identifier", blobId);

            var newVideoData = new VideoData()
            {
                UploadData = new()
                {
                    State = VideoUploadState.BlobOnly,
                    // TODO
                    UploadStrategyId = Guid.AllBitsSet,
                    BlobData = new()
                    {
                        BlobId = blobId,
                        SanitizationData = new()
                        {
                            State = BlobSanitizationState.Unsanitized,
                        }
                    }

                },
                // VideoFormat = null: not set until signature is confirmed
                Orientation = VideoOrientation.Unidentified,
                AvailableImageQuality = []
            };

            await _videoDataRepository.AddVideoDataAsync(newVideoData, cancellationToken);
            await _videoDataRepository.SaveChangesAsync(cancellationToken);

            await _orchestrator.RequestSanitizationAsync(newVideoData, cancellationToken);
        }
    }
}
