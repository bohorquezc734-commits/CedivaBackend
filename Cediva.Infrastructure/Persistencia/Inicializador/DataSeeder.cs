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
                // Verificamos conexión con la base de datos
                if (await context.Database.CanConnectAsync())
                {
                    // 1. Sembrado si la tabla Usuarios está completamente vacía
                    if (!await context.Usuarios.AnyAsync())
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
                            ),
                            new Usuario(
                                Guid.NewGuid(),
                                "Auxiliar Logístico y Picking",
                                "auxiliar@cediva.com",
                                passwordHasher.Hash("Auxiliar123*"),
                                RolUsuario.AuxiliarLogistico
                            )
                        };

                        await context.Usuarios.AddRangeAsync(usuariosIniciales);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        // 2. Si ya existen usuarios pero aún no se ha sembrado el usuario AuxiliarLogistico
                        if (!await context.Usuarios.AnyAsync(u => u.Email == "auxiliar@cediva.com"))
                        {
                            var auxiliar = new Usuario(
                                Guid.NewGuid(),
                                "Auxiliar Logístico y Picking",
                                "auxiliar@cediva.com",
                                passwordHasher.Hash("Auxiliar123*"),
                                RolUsuario.AuxiliarLogistico
                            );

                            await context.Usuarios.AddAsync(auxiliar);
                            await context.SaveChangesAsync();
                        }
                    }
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
