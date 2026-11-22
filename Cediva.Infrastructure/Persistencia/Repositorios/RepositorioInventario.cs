using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
using Cediva.Domain.Agregados.Inventario;
using Cediva.Dominio.Interfaces;

namespace Cediva.Infrastructure.Persistencia.Repositorios
{
    public class RepositorioInventario : RepositorioBase<Inventario>, IRepositorioInventario
    {
        public RepositorioInventario(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Inventario?> ObtenerPorProducto(Guid productoId, CancellationToken cancellationToken = default)
        {
  
            return await _context.Inventarios
                .FirstOrDefaultAsync(i => i.ProductoId == productoId, cancellationToken);
        }

        public async Task<List<Inventario>> ObtenerConStockBajo(int umbral, CancellationToken cancellationToken = default)
        {
          
            return await _context.Inventarios
                .Where(i => i.CantidadDisponible.Valor <= umbral)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Inventario>> ObtenerPorUbicacion(string ubicacion, CancellationToken cancellationToken = default)
        {
          
            return await Task.FromResult(new List<Inventario>());
        }
    }
}