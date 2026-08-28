using Cediva.Domain.Agregados.Inventario;
using Cediva.Domain.Enumeraciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cediva.Infrastructure.Persistencia.Configuraciones;

public class InventarioConfiguracion : IEntityTypeConfiguration<Inventario>
{
    public void Configure(EntityTypeBuilder<Inventario> builder)
    {
        builder.ToTable("Inventarios");

        builder.HasKey(i => i.Id);

        // Relación implícita con Producto
        // builder.Property("ProductoId").IsRequired();

        // Value Object Compuesto: Cantidad (Ej. CantidadDisponible, asumiendo su existencia estándar)
        builder.OwnsOne(i => i.CantidadDisponible, cantidad =>
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

        // Relación 1:N con Movimientos
        builder.HasMany(i => i.Movimientos)
            .WithOne() // Sin navegación recíproca pura si es agregado cerrado (DDD)
            .HasForeignKey("InventarioId") // Foreign key en base de datos
            .OnDelete(DeleteBehavior.Cascade);
            
        // EF Core 8: Acceso al backing field si la colección está encapsulada (asumiendo _movimientos interno)
        builder.Metadata.FindNavigation(nameof(Inventario.Movimientos))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
