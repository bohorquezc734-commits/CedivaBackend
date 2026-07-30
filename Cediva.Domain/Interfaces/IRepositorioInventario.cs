using Cediva.Domain.Agregados.Inventario;
using Cediva.Dominio.Interfaces;

namespace Cediva.Dominio.Interfaces;

public interface IRepositorioInventario : IRepositorio<Inventario>
{
    Task<Inventario?> ObtenerPorProducto(Guid productoId, CancellationToken cancellationToken = default);
    Task<List<Inventario>> ObtenerConStockBajo(int umbral, CancellationToken cancellationToken = default);
    Task<List<Inventario>> ObtenerPorUbicacion(string ubicacion, CancellationToken cancellationToken = default);
}