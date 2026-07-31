using Cediva.Domain.Agregados.Inventario;
using Cediva.Dominio.Interfaces;

namespace Cediva.Infrastructure.Persistencia.Repositorios
{
    public class RepositorioInventario : RepositorioBase<Inventario>, IRepositorioInventario
    {
        public RepositorioInventario(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Inventario>> ObtenerConStockBajo(int umbral, CancellationToken cancellationToken = default)
        {
            // TODO: Implement when Inventario properties are defined
            throw new NotImplementedException();
        }

        public Task<Inventario?> ObtenerPorProducto(Guid productoId, CancellationToken cancellationToken = default)
        {
            // TODO: Implement when Inventario properties are defined
            throw new NotImplementedException();
        }

        public Task<List<Inventario>> ObtenerPorUbicacion(string ubicacion, CancellationToken cancellationToken = default)
        {
            // TODO: Implement when Inventario properties are defined
            throw new NotImplementedException();
        }
    }
}
