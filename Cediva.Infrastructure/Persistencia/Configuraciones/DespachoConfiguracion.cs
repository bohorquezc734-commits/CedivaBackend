using Cediva.Domain.Agregados.Despacho;
using Cediva.Domain.Enumeraciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cediva.Infrastructure.Persistencia.Configuraciones;

public class DespachoConfiguracion : IEntityTypeConfiguration<Despacho>
{
    public void Configure(EntityTypeBuilder<Despacho> builder)
    {
        builder.ToTable("Despachos");

        builder.HasKey(d => d.Id);

        // Enum: EstadoDespacho
        builder.Property(d => d.Estado)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        // Value Object Compuesto: Direccion
        builder.OwnsOne(d => d.DireccionEntrega, dir =>
        {
            dir.Property(x => x.Calle)
                .HasColumnName("Direccion_Calle")
                .HasMaxLength(150)
                .IsRequired();

            dir.Property(x => x.Numero)
                .HasColumnName("Direccion_Numero")
                .HasMaxLength(20)
                .IsRequired();

            dir.Property(x => x.Complemento)
                .HasColumnName("Direccion_Complemento")
                .HasMaxLength(50);

            dir.Property(x => x.Ciudad)
                .HasColumnName("Direccion_Ciudad")
                .HasMaxLength(100)
                .IsRequired();

            dir.Property(x => x.Departamento)
                .HasColumnName("Direccion_Departamento")
                .HasMaxLength(100)
                .IsRequired();

            dir.Property(x => x.Pais)
                .HasColumnName("Direccion_Pais")
                .HasMaxLength(100)
                .IsRequired();

            dir.Property(x => x.CodigoPostal)
                .HasColumnName("Direccion_CodigoPostal")
                .HasMaxLength(20);
        });

        // Relación 1:N con ItemsDespacho
        builder.HasMany(d => d.Items)
            .WithOne()
            .HasForeignKey("DespachoId")
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.Metadata.FindNavigation(nameof(Despacho.Items))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
