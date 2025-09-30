using Kronos.Machina.Application.Misc.Sanitization;
using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Domain.Repositories;
using Kronos.Machina.Infrastructure.ConfigOptions;
using Kronos.Machina.Infrastructure.Jobs.Sanitization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using System.Diagnostics;

namespace Kronos.Machina.Infrastructure.Misc.Sanitization
{
    public class BlobSanitizationOrchestrator : IBlobSanitizationOrchestrator
    {
        private readonly ILogger<BlobSanitizationOrchestrator> _logger;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly BlobSanitizationStageFactory _stageFactory;
        private readonly PipelineConfig _pipelineConfig;
        private readonly IVideoDataRepository _videoDataRepository;

        public BlobSanitizationOrchestrator(ILogger<BlobSanitizationOrchestrator> logger,
            ISchedulerFactory schedulerFactory,
            BlobSanitizationStageFactory stageFactory,
            IOptions<PipelineConfig> options,
            IVideoDataRepository videoDataRepository)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
            _stageFactory = stageFactory;
            _pipelineConfig = options.Value;
            _videoDataRepository = videoDataRepository;
        }

        public async Task InitializeSanitizationAsync(VideoData videoData,
            CancellationToken cancellationToken = default)
        {
            Debug.Assert(videoData.UploadData != null);
            Debug.Assert(videoData.UploadData.BlobData != null);
            Debug.Assert(videoData.UploadData.BlobData.SanitizationData != null);


            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

            _logger.LogInformation("Initializing sanitizaion cycle for VideoData {id}...", videoData.Id);

            var stageId = _pipelineConfig.Stages
                .Single(s => s.Order == 0).Id;

            var stage = _stageFactory.GetStageInstance(stageId);

            var jobId = $"{stageId}-{videoData.Id}";

            (var job, var trigger) = stage
                .GetExucatables(jobId, ("VideoDataId", videoData.Id.ToString()));


            try
            {
                await scheduler.ScheduleJob(job, trigger, cancellationToken);

                _logger.LogInformation("Initialization success for VideoData {id}", videoData.Id);
                _logger.LogInformation("First stage of sanitizaion for VideoData {id} has begun", videoData.Id);

                videoData.UploadData.BlobData.SanitizationData.NextStageNumber += 1;
                _videoDataRepository.AttachModified(videoData);
                await _videoDataRepository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Initialization failure for VideoData {id}: {err}",
                    videoData.Id, ex.Message);
            }
        }

        public async Task RequestActionAsync(SanitizationStageResult stageResult,
            CancellationToken cancellationToken = default)
        {
            if (stageResult.IsTerminal && !stageResult.IsInvalidStateResolutionStage)
            {
                stageResult.VideoData.UploadData.State = VideoUploadState.SanitizedBlob;
                stageResult.VideoData.UploadData.BlobData.SanitizationData
                    .History.AddEntry("Sanitization done, ready for processing", true);

                _videoDataRepository.AttachModified(stageResult.VideoData);
                await _videoDataRepository.SaveChangesAsync(cancellationToken);

                return;
            }


            if (stageResult.IsInvalidStateResolutionStage)
            {
                stageResult.VideoData.UploadData.State = VideoUploadState.Invalid;
                stageResult.VideoData.UploadData.BlobData.SanitizationData
                    .History.AddEntry("Sanitization interrupted, invalid file", false);

                _videoDataRepository.AttachModified(stageResult.VideoData);
                await _videoDataRepository.SaveChangesAsync(cancellationToken);

                return;
            }


            if (!stageResult.IsSuccessful)
            {
                var stageId = "InvalidBlob";

                var stage = _stageFactory.GetStageInstance(stageId);

                var jobId = $"{stageId}-{stageResult.VideoData.Id}";

                (var job, var trigger) = stage
                    .GetExucatables(jobId, ("VideoDataId", stageResult.VideoData.Id.ToString()));

                try
                {
                    var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

                    _logger.LogInformation("Launching processing for invalid blob for VideoData {id}",
                        stageResult.VideoData.Id);

                    await scheduler.ScheduleJob(job, trigger, cancellationToken);

                    _logger.LogInformation("Processing for invalid blob for VideoData {id} launch success",
                        stageResult.VideoData.Id);

                    stageResult.VideoData.UploadData.BlobData.SanitizationData.NextStageNumber += 1;
                    _videoDataRepository.AttachModified(stageResult.VideoData);
                    await _videoDataRepository.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Initialization failure for VideoData {id}: {err}",
                        stageResult.VideoData.Id, ex.Message);
                }
            }
            else
            {
                var nextStageNumber = stageResult
                    .VideoData
                    .UploadData
                    .BlobData
                    .SanitizationData
                    .NextStageNumber;

                var stageConfig = _pipelineConfig.Stages
                    .SingleOrDefault
                    (
                        s => s.Order == nextStageNumber + 1
                    );

                if (stageConfig == null)
                {
                    stageResult.VideoData.UploadData.State = VideoUploadState.SanitizedBlob;
                    stageResult.VideoData.UploadData.BlobData.SanitizationData
                        .History.AddEntry("Sanitization done, ready for processing", true);

                    _videoDataRepository.AttachModified(stageResult.VideoData);
                    await _videoDataRepository.SaveChangesAsync(cancellationToken);

                    return;
                }

                var stage = _stageFactory.GetStageInstance(stageConfig.Id);

                var jobId = $"{stageConfig.Id}-{stageResult.VideoData.Id}";

                (var job, var trigger) = stage
                    .GetExucatables(jobId, ("VideoDataId", stageResult.VideoData.Id.ToString()));

                try
                {
                    var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

                    _logger.LogInformation("Launching sanitization stage #{num} for VideoData {id}",
                        nextStageNumber, stageResult.VideoData.Id);

                    await scheduler.ScheduleJob(job, trigger, cancellationToken);

                    _logger.LogInformation("#{num} stage of sanitizaion for VideoData {id} has begun",
                        nextStageNumber, stageResult.VideoData.Id);

                    stageResult.VideoData.UploadData.BlobData.SanitizationData.NextStageNumber += 1;
                    _videoDataRepository.AttachModified(stageResult.VideoData);
                    await _videoDataRepository.SaveChangesAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError("#{num} stage failure for VideoData {id}: {err}",
                        nextStageNumber, stageResult.VideoData.Id, ex.Message);
                }
            }
        }
    }
}
