using Kronos.Machina.Application.Handlers;
using Kronos.Machina.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;

namespace Kronos.Machina.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR
            (
                cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(VideoDataMapperProfile)));

            return services;
        }
    }
}
