using Kronos.Machina.Application.Misc.Sanitization;
using Kronos.Machina.Domain.Entities;
using Kronos.Machina.Infrastructure.Jobs;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System.Diagnostics;

namespace Kronos.Machina.Infrastructure.Misc.Sanitization
{
    public class BlobSanitizationOrchestrator : IBlobSanitizationOrchestrator
    {
        private readonly ILogger<BlobSanitizationOrchestrator> _logger;
        private readonly ISchedulerFactory _schedulerFactory;

        public BlobSanitizationOrchestrator(ILogger<BlobSanitizationOrchestrator> logger, 
            ISchedulerFactory schedulerFactory)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
        }

        public async Task InitializeSanitizationAsync(VideoData videoData, 
            CancellationToken cancellationToken = default)
        {
            Debug.Assert(videoData.UploadData != null);
            Debug.Assert(videoData.UploadData.BlobData != null);
            Debug.Assert(videoData.UploadData.BlobData.SanitizationData != null);

            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

            _logger.LogInformation("Started sanitizaion cycle init for VideoData {id}", videoData.Id);

            var job = JobBuilder.Create<SignatureValidationBlobSanitizationJob>()
                .WithIdentity("signatureValidationBlobSanitizationJob")
                .UsingJobData("VideoDataId", videoData.Id.ToString())
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("signatureValidationBlobSanitizationJobTrigger")
                .StartAt(DateTimeOffset.Now.AddSeconds(10))
                .Build();

            try
            {
                await scheduler.ScheduleJob(job, trigger, cancellationToken);

                _logger.LogInformation("Sanitizaion init for VideoData {id} was successful", videoData.Id);
                _logger.LogInformation("First stage of sanitizaion for VideoData {id} has begun", videoData.Id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Sanitizaion init for VideoData {id} failed: {err}", 
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
