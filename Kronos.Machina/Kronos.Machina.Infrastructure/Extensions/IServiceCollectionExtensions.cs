using Kronos.Machina.Application.Misc.Sanitization;
using Kronos.Machina.Infrastructure.ConfigOptions;
using Kronos.Machina.Infrastructure.Misc.Sanitization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kronos.Machina.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureImplementations(this IServiceCollection services)
        {
            services.AddScoped<IBlobSanitizationOrchestrator, BlobSanitizationOrchestrator>();

            return services;
        }
    }
}
