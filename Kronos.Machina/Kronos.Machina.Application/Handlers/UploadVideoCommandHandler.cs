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
        private readonly IBackgroundJobScheduler _scheduler;
        private readonly ILogger<UploadVideoCommandHandler> _logger;


        public UploadVideoCommandHandler(IVideoDataRepository videoDataRepository,
            IVideoBlobService videoBlobService,
            IBackgroundJobScheduler scheduler,
            ILogger<UploadVideoCommandHandler> logger)
        {
            _videoDataRepository = videoDataRepository;
            _videoBlobService = videoBlobService;
            _scheduler = scheduler;
            _logger = logger;
        }

        public async Task Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Blob upload about to start");

            var blobIdentifier = await _videoBlobService.SaveToBlob(request.Source);

            _logger.LogDebug("Blob uploaded to quarantine with {id} identifier", blobIdentifier);

            var newVideoData = new VideoData()
            {
                UploadData = new()
                {
                    State = VideoUploadState.BlobOnly,
                    UploadStrategyId = Guid.AllBitsSet,
                    BlobId = blobIdentifier

                },
                Orientation = VideoOrientation.Unidentified,
                AvailableImageQuality = []
            };

            await _videoDataRepository.AddVideoDataAsync(newVideoData, cancellationToken);

            await _scheduler.ScheduleSanitization(newVideoData.UploadData);
        }
    }
}
