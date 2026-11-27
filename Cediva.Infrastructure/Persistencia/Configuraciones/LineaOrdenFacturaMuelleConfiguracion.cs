using Cediva.Domain.Agregados.Muelle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cediva.Infrastructure.Persistencia.Configuraciones
{
    public class LineaOrdenFacturaMuelleConfiguracion : IEntityTypeConfiguration<LineaOrdenFacturaMuelle>
    {
        public void Configure(EntityTypeBuilder<LineaOrdenFacturaMuelle> builder)
        {
            builder.ToTable("LineasOrdenesFacturasMuelle");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.CodigoBarras)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(l => l.CantidadPedida)
                .IsRequired();

            builder.Property(l => l.CantidadAlistada)
                .IsRequired();

            builder.Property(l => l.CantidadPistoleada)
                .IsRequired();
        }
    }
}
