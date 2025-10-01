using FFMpegCore;
using Kronos.Machina.Application.Misc.Sanitization;
using Kronos.Machina.Contracts.CommonExceptions;
using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Domain.Repositories;
using Kronos.Machina.Infrastructure.Data.BlobStorage;
using Kronos.Machina.Infrastructure.Misc.Probing;
using Mapster;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Kronos.Machina.Infrastructure.Jobs.Sanitization
{
    public class ProbeAnalysisBlobSanitizationJob : IJob
    {
        private readonly IBlobStorage _blobStorage;
        private readonly IVideoDataRepository _videoDataRepository;
        private readonly IVideoFormatRepository _videoFormatRepository;
        private readonly IBlobSanitizationOrchestrator _orchestrator;
        private readonly IProbeAccessor _probeAccessor;
        private readonly ILogger<ProbeAnalysisBlobSanitizationJob> _logger;

        public ProbeAnalysisBlobSanitizationJob(IBlobStorage blobStorage,
            IVideoDataRepository videoDataRepository,
            IVideoFormatRepository videoFormatRepository,
            IBlobSanitizationOrchestrator blobSanitizationOrchestrator,
            IProbeAccessor probeAccessor,
            ILogger<ProbeAnalysisBlobSanitizationJob> logger)
        {
            _blobStorage = blobStorage;
            _videoDataRepository = videoDataRepository;
            _videoFormatRepository = videoFormatRepository;
            _orchestrator = blobSanitizationOrchestrator;
            _probeAccessor = probeAccessor;
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

            var blobIdentifier =
                DiskMemoryBlobIdentifier.Construct(videoData.UploadData.BlobData.BlobId);

            var blobPath = _blobStorage.GetAbsolutePath(blobIdentifier);

            try
            {
                _logger.LogInformation("Starting probing for VideoData {id}", videoDataId);

                var probeResult = _probeAccessor.Probe(blobPath);

                probeResult.Adapt(videoData.UploadData.BlobData);

                if (probeResult.Width > probeResult.Height)
                {
                    videoData.Orientation = VideoOrientation.Horizontal;
                } else
                {
                    videoData.Orientation = VideoOrientation.Vertical;
                }

                videoData.UploadData.BlobData.SanitizationData.State =
                   BlobSanitizationState.FormatConfirmed;

                videoData.UploadData.BlobData.SanitizationData
                    .History.AddEntry($"Format confirmed with probing, video codec: " +
                        $"{probeResult.VideoCodecName}", true);

                _logger.LogInformation("Probing success for VideoData {id}", videoDataId);

                await _videoDataRepository.SaveChangesAsync();

                await _orchestrator.RequestActionAsync
                (
                    new SanitizationStageResult
                    {
                        IsSuccessful = true,
                        VideoData = videoData,
                        StageType = this.GetType(),
                    }
                );
            }
            catch (Exception ex)
            {
                videoData.UploadData.BlobData.SanitizationData
                    .History.AddEntry("Format did not verify probation", false);

                _logger.LogError("Probing failure for VideoData, error: {err}", ex.Message);

                await _orchestrator.RequestActionAsync
                (
                    new SanitizationStageResult
                    {
                        IsSuccessful = false,
                        VideoData = videoData,
                        StageType = this.GetType(),
                        Exception = ex
                    }
                );
            }
        }
    }
}
