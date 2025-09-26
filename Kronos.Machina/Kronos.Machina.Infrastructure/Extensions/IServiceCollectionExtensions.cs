using Kronos.Machina.Application.Misc.Sanitization;
using Kronos.Machina.Application.Services;
using Kronos.Machina.Infrastructure.ConfigOptions;
using Kronos.Machina.Infrastructure.Data.BlobStorage;
using Kronos.Machina.Infrastructure.Misc.Sanitization;
using Kronos.Machina.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kronos.Machina.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureImplementations(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IBlobStorage, DiskMemoryBlobStorage>();
            services.AddScoped<IBlobSanitizationOrchestrator, BlobSanitizationOrchestrator>();

            services.Configure<SanitizedBlobZoneInfo>(configuration.GetSection("SanitizedZone"));
            services.Configure<UnsanitizedBlobZoneInfo>(configuration.GetSection("UnsanitizedZone"));

            return services;
        }
    }
}
