using Cediva.Dominio.Agregados.Producto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cediva.Infrastructure.Persistencia.Configuraciones
{
    public class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Descripcion)
                .HasMaxLength(500);

            builder.OwnsOne(p => p.CodigoBarras, cb =>
            {
                cb.Property(c => c.Valor)
                  .HasColumnName("CodigoBarras")
                  .HasMaxLength(20)
                  .IsRequired();
            });

            builder.Property(p => p.PrecioUnitario)
                .HasColumnType("decimal(18,2)");
                
            builder.Property(p => p.UnidadMedida)
                .IsRequired();

            builder.Property(p => p.ImagenUrl)
                .HasMaxLength(500);

            builder.HasOne(p => p.Categoria)
                .WithMany()
                .HasForeignKey("CategoriaId")
                .IsRequired();

            builder.HasOne(p => p.Marca)
                .WithMany()
                .HasForeignKey("MarcaId")
                .IsRequired(false);
        }
    }
}
