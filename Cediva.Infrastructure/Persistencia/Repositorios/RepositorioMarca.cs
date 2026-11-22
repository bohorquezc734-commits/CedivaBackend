using Cediva.Dominio.Agregados.Producto;
using Cediva.Dominio.Interfaces;

namespace Cediva.Infrastructure.Persistencia.Repositorios
{
    public class RepositorioMarca : RepositorioBase<Marca>, IRepositorioMarca
    {
        public RepositorioMarca(ApplicationDbContext context) : base(context)
        {
        }
    }
}
