using Cediva.Dominio.Interfaces;
using Cediva.Infrastructure.Persistencia;
using Cediva.Infrastructure.Persistencia.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cediva.Infrastructure
{
    public static class InyeccionDependencias
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IRepositorioProducto, RepositorioProducto>();
            services.AddScoped<IRepositorioInventario, RepositorioInventario>();
            services.AddScoped<IRepositorioDespacho, RepositorioDespacho>();
            
            return services;
        }
    }
}
