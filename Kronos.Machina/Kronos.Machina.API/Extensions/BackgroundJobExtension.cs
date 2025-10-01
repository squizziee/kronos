using Kronos.Machina.Infrastructure.Jobs.FFmpeg;
using Quartz;

namespace Kronos.Machina.API.Extensions
{
    public static class BackgroundJobExtension
    {
        public static IServiceCollection AddBackgroundJobProvider(this IServiceCollection services)
        {
            services.AddQuartz(opt =>
            {
                opt.SchedulerId = "MachinaScheduler0000";
                opt.SchedulerName = "Default";

                opt.UseSimpleTypeLoader();
                opt.UseInMemoryStore();
                opt.UseDefaultThreadPool(tp => tp.MaxConcurrency = 10);

                //var jobKey = new JobKey("ffmpeg-util-start");

                //opt.AddJob<FFmpegUtilsLaunchJob>(job => job.WithIdentity(jobKey));
                //opt.AddTrigger(opts => opts.ForJob(jobKey).StartNow());
            });

            services.AddQuartzHostedService(
                opt => 
                {
                    opt.WaitForJobsToComplete = true;
                }
            );

            // Shut FFmpeg utils down on exit
            //AppDomain.CurrentDomain.ProcessExit += (_,_) => FFmpegUtilsLaunchJob.KillProcess();

            return services;
        }
    }
}
