using Cediva.Domain.Enumeraciones;
using Cediva.Dominio.Agregados.Producto;
using Cediva.Dominio.ObjetosValor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cediva.Infrastructure.Persistencia.Configuraciones;

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

        // Value Object: CodigoBarras (Single Primitive)
        builder.Property(p => p.CodigoBarras)
            .HasConversion(
                codigo => codigo.Valor,
                valor => new CodigoBarras(valor))
            .IsRequired()
            .HasMaxLength(50);
            
        builder.HasIndex(p => p.CodigoBarras).IsUnique();

        builder.Property(p => p.PrecioUnitario)
            .HasPrecision(18, 2)
            .IsRequired();

        // Enum: UnidadMedida
        builder.Property(p => p.UnidadMedida)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.EstaActivo).IsRequired();
        builder.Property(p => p.FechaCreacion).IsRequired();
    }
}
