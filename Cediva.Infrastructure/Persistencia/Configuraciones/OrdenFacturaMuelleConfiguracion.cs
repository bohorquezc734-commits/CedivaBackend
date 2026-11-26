using Cediva.Domain.Agregados.Muelle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cediva.Infrastructure.Persistencia.Configuraciones
{
    public class OrdenFacturaMuelleConfiguracion : IEntityTypeConfiguration<OrdenFacturaMuelle>
    {
        public void Configure(EntityTypeBuilder<OrdenFacturaMuelle> builder)
        {
            builder.ToTable("OrdenesFacturasMuelle");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.NumeroFactura)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(o => o.NumeroFactura)
                .IsUnique();

            builder.Property(o => o.ClienteNombre)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(o => o.ClienteNit)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.Muelle)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.Estado)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.FechaCreacion)
                .IsRequired();

            builder.Property(o => o.AuditadoPor)
                .HasMaxLength(100);

            builder.Property(o => o.HoraAuditoria)
                .HasMaxLength(20);

            builder.Property(o => o.DespachadoPor)
                .HasMaxLength(100);

            builder.Property(o => o.HoraDespacho)
                .HasMaxLength(20);

            builder.Property(o => o.Transportadora)
                .HasMaxLength(100);

            builder.Property(o => o.PlacaVehiculo)
                .HasMaxLength(20);

            builder.Property(o => o.Conductor)
                .HasMaxLength(100);

            // Relación 1:N con Lineas de la orden
            builder.HasMany(o => o.Lineas)
                .WithOne()
                .HasForeignKey(l => l.OrdenFacturaMuelleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata.FindNavigation(nameof(OrdenFacturaMuelle.Lineas))
                ?.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
