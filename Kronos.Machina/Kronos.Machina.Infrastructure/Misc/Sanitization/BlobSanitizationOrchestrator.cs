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

        public BlobSanitizationOrchestrator(ILogger<BlobSanitizationOrchestrator> logger)
        {
            _logger = logger;
        }

        public async Task InitializeSanitizationAsync(VideoData videoData, 
            CancellationToken cancellationToken = default)
        {
            Debug.Assert(videoData.UploadData != null);
            Debug.Assert(videoData.UploadData.BlobData != null);
            Debug.Assert(videoData.UploadData.BlobData.SanitizationData != null);

            var scheduler = await StdSchedulerFactory.GetDefaultScheduler(cancellationToken);

            _logger.LogInformation("Started sanitizaion cycle init for VideoData {id}", videoData.Id);

            var job = JobBuilder.Create<SignatureValidationBlobSanitizationJob>()
                .WithIdentity("signatureValidationBlobSanitizationJob")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("signatureValidationBlobSanitizationJobTrigger")
                .StartNow()
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
