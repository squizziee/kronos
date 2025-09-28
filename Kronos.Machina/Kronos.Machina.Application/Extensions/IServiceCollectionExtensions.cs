using Kronos.Machina.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

            return services;
        }
    }
}
