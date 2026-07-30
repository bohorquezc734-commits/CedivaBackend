namespace Cediva.Dominio.Interfaces;

public interface IRepositorio<T> where T : IAggregateRoot
{
    Task<T?> ObtenerPorId(Guid id, CancellationToken cancellationToken = default);
    Task<List<T>> ObtenerTodos(CancellationToken cancellationToken = default);
    Task Agregar(T entidad, CancellationToken cancellationToken = default);
    Task Actualizar(T entidad, CancellationToken cancellationToken = default);
    Task Eliminar(T entidad, CancellationToken cancellationToken = default);
}