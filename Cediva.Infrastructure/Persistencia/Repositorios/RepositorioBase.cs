using Cediva.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cediva.Infrastructure.Persistencia.Repositorios
{
    public class RepositorioBase<T> : IRepositorio<T> where T : class, IAggregateRoot
    {
        protected readonly ApplicationDbContext _context;

        public RepositorioBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Actualizar(T entidad, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Update(entidad);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Agregar(T entidad, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entidad, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Eliminar(T entidad, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entidad);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T?> ObtenerPorId(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<List<T>> ObtenerTodos(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }
    }
}
