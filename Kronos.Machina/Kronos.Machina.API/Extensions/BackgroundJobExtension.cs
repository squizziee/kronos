using Quartz;

namespace Kronos.Machina.API.Extensions
{
    public static class BackgroundJobExtension
    {
        public static IServiceCollection AddBackgroundJobProvider(this IServiceCollection services)
        {
            services.AddQuartz();
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
