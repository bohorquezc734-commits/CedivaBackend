using Cediva.Application.Comunes.Interfaces;
using Cediva.Domain.Interfaces;
using Cediva.Dominio.Interfaces;
using Cediva.Infrastructure.Persistencia;
using Cediva.Infrastructure.Persistencia.Repositorios;
using Cediva.Infrastructure.Seguridad;
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

            // Configuración de JWT Options sin requerir paquetes de Binding externos
            services.Configure<JwtSettings>(options =>
            {
                var section = configuration.GetSection(JwtSettings.SectionName);
                options.SecretKey = section["SecretKey"] ?? string.Empty;
                options.Issuer = section["Issuer"] ?? string.Empty;
                options.Audience = section["Audience"] ?? string.Empty;
                if (int.TryParse(section["ExpirationMinutes"], out var exp))
                {
                    options.ExpirationMinutes = exp;
                }
            });

            // Servicios de Seguridad
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // Repositorios
            services.AddScoped<IRepositorioProducto, RepositorioProducto>();
            services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();
            services.AddScoped<IRepositorioMarca, RepositorioMarca>();
            services.AddScoped<IRepositorioInventario, RepositorioInventario>();
            services.AddScoped<IRepositorioDespacho, RepositorioDespacho>();
            services.AddScoped<IRepositorioCliente, RepositorioCliente>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IOrdenFacturaMuelleRepository, OrdenFacturaMuelleRepository>();
            
            return services;
        }
    }
}
