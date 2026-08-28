using Cediva.Dominio.Agregados.Producto;
using Cediva.Domain.Agregados.Inventario;
using Cediva.Domain.Agregados.Despacho;
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
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
