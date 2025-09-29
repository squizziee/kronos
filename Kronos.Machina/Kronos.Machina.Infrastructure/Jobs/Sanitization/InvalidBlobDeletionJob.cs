using Kronos.Machina.Application.Misc.Sanitization;
using Kronos.Machina.Contracts.CommonExceptions;
using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Domain.Repositories;
using Kronos.Machina.Infrastructure.Data.BlobStorage;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Kronos.Machina.Infrastructure.Jobs.Sanitization
{
    public class InvalidBlobDeletionJob : IJob
    {
        private readonly IBlobStorage _blobStorage;
        private readonly IVideoDataRepository _videoDataRepository;
        private readonly IVideoFormatRepository _videoFormatRepository;
        private readonly IBlobSanitizationOrchestrator _orchestrator;
        private readonly ILogger<InvalidBlobDeletionJob> _logger;

        public InvalidBlobDeletionJob(IBlobStorage blobStorage,
            IVideoDataRepository videoDataRepository,
            IVideoFormatRepository videoFormatRepository,
            IBlobSanitizationOrchestrator blobSanitizationOrchestrator,
            ILogger<InvalidBlobDeletionJob> logger)
        {
            _blobStorage = blobStorage;
            _videoDataRepository = videoDataRepository;
            _videoFormatRepository = videoFormatRepository;
            _orchestrator = blobSanitizationOrchestrator;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var videoDataStrGuid = context.JobDetail.JobDataMap.GetString("VideoDataId");

            if (!Guid.TryParse(videoDataStrGuid, out var videoDataId))
            {
                throw new ArgumentException("VideoDataId provided is not a valid Guid");
            }

            var videoData = await _videoDataRepository.GetByIdAsync(videoDataId,
                context.CancellationToken);

            if (videoData == null)
            {
                throw new ResourceNotFoundException($"No video data with Guid {videoData} was found");
            }

            var blobIdentifier = DiskMemoryBlobIdentifier.Construct(videoData.UploadData.BlobData.BlobId);

            try
            {
                _logger.LogInformation("Launching invalid blob deletion for VideoData {id}",
                        videoDataId);

                await _blobStorage.RemoveBlobFromStorageAsync(blobIdentifier, context.CancellationToken);

                _logger.LogInformation("Invalid blob deleted for VideoData {id}",
                       videoDataId);

                videoData.UploadData.BlobData.SanitizationData
                    .History.AddEntry($"Invalid blob deleted from storage", true);

                videoData.UploadData.State = VideoUploadState.Invalid;
                await _videoDataRepository.SaveChangesAsync();

                await _orchestrator.RequestActionAsync
                (
                    new SanitizationStageResult()
                    {
                        IsSuccessful = true,
                        VideoData = videoData,
                        StageType = this.GetType(),
                        IsTerminal = true,
                        IsInvalidStateResolutionStage = true,
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Could not delete blob for VideoData {id}: {err}", videoData.Id, ex.Message);

                await _orchestrator.RequestActionAsync
                (
                    new SanitizationStageResult()
                    {
                        IsSuccessful = false,
                        VideoData = videoData,
                        StageType = this.GetType(),
                        Exception = ex,
                        IsTerminal = true
                    }
                );
            }
        }
    }
}
