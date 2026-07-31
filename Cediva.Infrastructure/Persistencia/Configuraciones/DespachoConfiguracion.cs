using Cediva.Domain.Agregados.Despacho;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cediva.Infrastructure.Persistencia.Configuraciones
{
    public class DespachoConfiguracion : IEntityTypeConfiguration<Despacho>
    {
        public void Configure(EntityTypeBuilder<Despacho> builder)
        {
            builder.ToTable("Despachos");
            builder.HasKey(d => d.Id);
        }
    }
}
