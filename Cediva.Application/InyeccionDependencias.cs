using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Cediva.Application
{
    public static class InyeccionDependencias
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
