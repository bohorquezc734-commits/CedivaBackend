using Cediva.Dominio.Agregados.Producto;
using Cediva.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cediva.Infrastructure.Persistencia.Repositorios
{
    public class RepositorioProducto : RepositorioBase<Producto>, IRepositorioProducto
    {
        public RepositorioProducto(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ExisteCodigoBarras(string codigoBarras, CancellationToken cancellationToken = default)
        {
            return await _context.Productos
                .AnyAsync(p => p.CodigoBarras.Valor == codigoBarras, cancellationToken);
        }

        public async Task<List<Producto>> ObtenerPorCategoria(Guid categoriaId, CancellationToken cancellationToken = default)
        {
            return await _context.Productos
                .Where(p => p.Categoria.Id == categoriaId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Producto?> ObtenerPorCodigoBarras(string codigoBarras, CancellationToken cancellationToken = default)
        {
            return await _context.Productos
                .FirstOrDefaultAsync(p => p.CodigoBarras.Valor == codigoBarras, cancellationToken);
        }

        public async Task<Producto?> ObtenerPorNombre(string nombre, CancellationToken cancellationToken = default)
        {
            return await _context.Productos
                .FirstOrDefaultAsync(p => p.Nombre == nombre, cancellationToken);
        }
    }
}
