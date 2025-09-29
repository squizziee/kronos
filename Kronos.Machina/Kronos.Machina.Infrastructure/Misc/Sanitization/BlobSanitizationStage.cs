using Kronos.Machina.Domain.Entities;
using Quartz;

namespace Kronos.Machina.Infrastructure.Misc.Sanitization
{
    public class BlobSanitizationStage
    {
        public required Type StageType { get; set; }

        public (IJobDetail job, ITrigger trigger) GetExucatables(string jobId, (string, string) jobArgs)
        {
            var job = JobBuilder.Create(StageType)
                .WithIdentity(jobId)
                .UsingJobData(jobArgs.Item1, jobArgs.Item2)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobId}-trigger")
                .StartNow()
                .Build();

            return (job, trigger);
        }
    }
}
