using System.Reflection;
using Cediva.Domain.Agregados.Despacho;
using Cediva.Domain.Agregados.Inventario;
using Cediva.Dominio.Agregados.Producto;
using Microsoft.EntityFrameworkCore;

namespace Cediva.Infrastructure.Persistencia
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Inventario> Inventarios { get; set; } = null!;
        public DbSet<Despacho> Despachos { get; set; } = null!;
        public DbSet<ItemDespacho> ItemsDespacho { get; set; } = null!;
        public DbSet<Movimiento> Movimientos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
