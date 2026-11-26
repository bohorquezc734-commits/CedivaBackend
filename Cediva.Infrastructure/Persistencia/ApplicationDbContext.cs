using System.Reflection;
using Cediva.Domain.Agregados.Cliente;
using Cediva.Domain.Agregados.Despacho;
using Cediva.Domain.Agregados.Inventario;
using Cediva.Domain.Agregados.Muelle;
using Cediva.Domain.Agregados.Usuario;
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
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Marca> Marcas { get; set; } = null!;
        public DbSet<Inventario> Inventarios { get; set; } = null!;
        public DbSet<Despacho> Despachos { get; set; } = null!;
        public DbSet<ItemDespacho> ItemsDespacho { get; set; } = null!;
        public DbSet<Movimiento> Movimientos { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<OrdenFacturaMuelle> OrdenesFacturasMuelle { get; set; } = null!;
        public DbSet<LineaOrdenFacturaMuelle> LineasOrdenesFacturasMuelle { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
