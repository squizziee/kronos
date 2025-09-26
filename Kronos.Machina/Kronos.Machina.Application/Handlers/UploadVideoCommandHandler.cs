using Kronos.Machina.Application.Misc.Sanitization;
using Kronos.Machina.Application.Services;
using Kronos.Machina.Contracts.Commands;
using Kronos.Machina.Contracts.Dto;
using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kronos.Machina.Application.Handlers
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, UploadVideoCommandResponseDto>
    {
        private readonly IVideoDataRepository _videoDataRepository;
        private readonly IBlobService _videoBlobService;
        private readonly IBlobSanitizationOrchestrator _orchestrator;
        private readonly ILogger<UploadVideoCommandHandler> _logger;


        public UploadVideoCommandHandler(IVideoDataRepository videoDataRepository,
            IBlobService videoBlobService,
            IBlobSanitizationOrchestrator orchestrator,
            ILogger<UploadVideoCommandHandler> logger)
        {
            _videoDataRepository = videoDataRepository;
            _videoBlobService = videoBlobService;
            _orchestrator = orchestrator;
            _logger = logger;
        }

        public async Task<UploadVideoCommandResponseDto> Handle(UploadVideoCommand request, 
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("Blob upload about to start");

            var blobId = await _videoBlobService.SaveToBlob(request.Source, cancellationToken);

            if (blobId == null)
            {
                // TODO
                throw new Exception("oopsie");
            }

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

            await _orchestrator.InitializeSanitizationAsync(newVideoData, cancellationToken);

            return new UploadVideoCommandResponseDto { 
                VideoDataId = newVideoData.Id 
            };
        }
    }
}
