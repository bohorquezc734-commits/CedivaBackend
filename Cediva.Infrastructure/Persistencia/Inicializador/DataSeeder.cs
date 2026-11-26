using System;
using System.Threading.Tasks;
using Cediva.Application.Comunes.Interfaces;
using Cediva.Domain.Agregados.Usuario;
using Cediva.Domain.Enumeraciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cediva.Infrastructure.Persistencia.Inicializador
{
    public static class DataSeeder
    {
        public static async Task InicializarAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            try
            {
                // Verificamos conexión y si la tabla Usuarios existe y está vacía
                if (await context.Database.CanConnectAsync() && !await context.Usuarios.AnyAsync())
                {
                    var usuariosIniciales = new[]
                    {
                        new Usuario(
                            Guid.NewGuid(),
                            "Administrador General",
                            "admin@cediva.com",
                            passwordHasher.Hash("Admin123*"),
                            RolUsuario.Administrador
                        ),
                        new Usuario(
                            Guid.NewGuid(),
                            "Supervisor de Bodega",
                            "supervisor@cediva.com",
                            passwordHasher.Hash("Super123*"),
                            RolUsuario.Supervisor
                        ),
                        new Usuario(
                            Guid.NewGuid(),
                            "Auditor y Operador RF",
                            "auditor@cediva.com",
                            passwordHasher.Hash("Auditor123*"),
                            RolUsuario.AuditorBodega
                        )
                    };

                    await context.Usuarios.AddRangeAsync(usuariosIniciales);
                    await context.SaveChangesAsync();
                }
            }
            catch
            {
                // Si la migración aún no ha sido aplicada a la base de datos, 
                // se omite para no interrumpir el arranque de la API.
            }
        }
    }
}
