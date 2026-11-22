using Cediva.Domain.Agregados.Inventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cediva.Infrastructure.Persistencia.Configuraciones;

public class MovimientoConfiguracion : IEntityTypeConfiguration<Movimiento>
{
    public void Configure(EntityTypeBuilder<Movimiento> builder)
    {
        builder.ToTable("Movimientos");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Tipo)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(m => m.FechaMovimiento)
            .IsRequired();

        builder.OwnsOne(m => m.Cantidad, cantidad =>
        {
            cantidad.Property(c => c.Valor)
                .HasColumnName("Cantidad_Valor")
                .HasPrecision(18, 2)
                .IsRequired();

            cantidad.Property(c => c.Unidad)
                .HasColumnName("Cantidad_Unidad")
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();
        });
    }
}