using Kronos.Machina.Application.Misc.Sanitization;
using Kronos.Machina.Application.Services;
using Kronos.Machina.Domain.Repositories;
using Kronos.Machina.Infrastructure.ConfigOptions;
using Kronos.Machina.Infrastructure.Data;
using Kronos.Machina.Infrastructure.Data.BlobStorage;
using Kronos.Machina.Infrastructure.Data.Repositories;
using Kronos.Machina.Infrastructure.Misc.Sanitization;
using Kronos.Machina.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
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

            services.AddScoped<IVideoDataRepository, VideoDataRepository>();
            services.AddScoped<IVideoFormatRepository, VideoFormatRepository>();
            services.AddScoped<BlobSanitizationStageFactory>();

            services.Configure<SanitizedBlobZoneInfo>(configuration.GetSection("SanitizedZone"));
            services.Configure<UnsanitizedBlobZoneInfo>(configuration.GetSection("UnsanitizedZone"));
            services.Configure<PipelineConfig>(configuration.GetSection("Pipeline"));

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<VideoDatabaseContext>(opt =>
            {
                opt.UseSqlite("Data Source=data.sqlite3");
            });

            return services;
        }
    }
}
