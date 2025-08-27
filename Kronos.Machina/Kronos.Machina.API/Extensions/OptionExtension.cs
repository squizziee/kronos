using Kronos.Machina.Infrastructure.ConfigOptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kronos.Machina.API.Extensions
{
    public static class OptionExtension
    {
        public static IServiceCollection AddInfrastructureOptions(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<VideoTypeSignatures>(configuration.GetSection("VideoTypeSignatures"));

            return services;
        }
    }
}
