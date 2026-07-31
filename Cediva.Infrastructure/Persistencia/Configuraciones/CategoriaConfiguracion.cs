using Cediva.Dominio.Agregados.Producto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cediva.Infrastructure.Persistencia.Configuraciones
{
    public class CategoriaConfiguracion : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Descripcion)
                .HasMaxLength(200);

            builder.HasOne(c => c.CategoriaPadre)
                .WithMany()
                .HasForeignKey("CategoriaPadreId")
                .IsRequired(false);
        }
    }
}
