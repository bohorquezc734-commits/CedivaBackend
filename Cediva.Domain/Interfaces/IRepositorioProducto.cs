using Cediva.Dominio.Agregados.Producto;

namespace Cediva.Dominio.Interfaces;

public interface IRepositorioProducto : IRepositorio<Producto>
{
    Task<Producto?> ObtenerPorCodigoBarras(string codigoBarras, CancellationToken cancellationToken = default);
    Task<Producto?> ObtenerPorNombre(string nombre, CancellationToken cancellationToken = default);
    Task<List<Producto>> ObtenerPorCategoria(Guid categoriaId, CancellationToken cancellationToken = default);
    Task<bool> ExisteCodigoBarras(string codigoBarras, CancellationToken cancellationToken = default);
}