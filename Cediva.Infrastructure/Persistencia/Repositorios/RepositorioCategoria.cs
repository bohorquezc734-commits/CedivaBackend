using Cediva.Dominio.Agregados.Producto;
using Cediva.Dominio.Interfaces;

namespace Cediva.Infrastructure.Persistencia.Repositorios
{
    public class RepositorioCategoria : RepositorioBase<Categoria>, IRepositorioCategoria
    {
        public RepositorioCategoria(ApplicationDbContext context) : base(context)
        {
        }
    }
}
