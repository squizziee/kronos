using Kronos.Machina.Application.Misc.Sanitization;
using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Infrastructure.ConfigOptions;
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

        public BlobSanitizationOrchestrator(ILogger<BlobSanitizationOrchestrator> logger, 
            ISchedulerFactory schedulerFactory,
            BlobSanitizationStageFactory stageFactory,
            IOptions<PipelineConfig> options)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
            _stageFactory = stageFactory;
            _pipelineConfig = options.Value;
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
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Initialization failure for VideoData {id}: {err}",
                    videoData.Id, ex.Message);
            }
        }

        public Task RequestActionAsync(SanitizationStageResult stageResult,
            CancellationToken cancellationToken = default)
        {       
            throw new NotImplementedException();
        }
    }
}
