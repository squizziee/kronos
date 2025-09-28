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
            });
            services.AddQuartzHostedService(
                opt => 
                {
                    opt.WaitForJobsToComplete = true;
                }
            );

            return services;
        }
    }
}
