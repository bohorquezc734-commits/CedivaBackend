using Cediva.Domain.Agregados.Despacho;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cediva.Infrastructure.Persistencia.Configuraciones;

public class ItemDespachoConfiguracion : IEntityTypeConfiguration<ItemDespacho>
{
    public void Configure(EntityTypeBuilder<ItemDespacho> builder)
    {
        builder.ToTable("ItemDespachos");
        builder.HasKey(i => i.Id);

        builder.OwnsOne(i => i.Cantidad, cantidad =>
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
