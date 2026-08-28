using Cediva.Domain.Agregados.Inventario;
using Cediva.Domain.Enumeraciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cediva.Infrastructure.Persistencia.Configuraciones;

public class MovimientoConfiguracion : IEntityTypeConfiguration<Movimiento>
{
    public void Configure(EntityTypeBuilder<Movimiento> builder)
    {
        builder.ToTable("Movimientos");
        
        builder.HasKey(m => m.Id);

        // Enum: TipoMovimiento (Entrada, Salida, Ajuste...)
        builder.Property(m => m.TipoMovimiento)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();
            
        // Value Object Compuesto: Cantidad
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
