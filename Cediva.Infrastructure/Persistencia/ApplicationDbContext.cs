using Cediva.Domain.Agregados.Despacho;
using Cediva.Domain.Agregados.Inventario;
using Cediva.Dominio.Agregados.Producto;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cediva.Infrastructure.Persistencia
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Despacho> Despachos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations from the current assembly (Fluent API)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
