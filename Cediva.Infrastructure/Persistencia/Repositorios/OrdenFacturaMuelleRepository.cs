using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Domain.Agregados.Muelle;
using Cediva.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cediva.Infrastructure.Persistencia.Repositorios
{
    public class OrdenFacturaMuelleRepository : RepositorioBase<OrdenFacturaMuelle>, IOrdenFacturaMuelleRepository
    {
        public OrdenFacturaMuelleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<OrdenFacturaMuelle?> ObtenerPorIdConLineas(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.OrdenesFacturasMuelle
                .Include(o => o.Lineas)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<List<OrdenFacturaMuelle>> ObtenerTodosConLineas(CancellationToken cancellationToken = default)
        {
            return await _context.OrdenesFacturasMuelle
                .Include(o => o.Lineas)
                .OrderByDescending(o => o.FechaCreacion)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExisteNumeroFactura(string numeroFactura, CancellationToken cancellationToken = default)
        {
            var facturaLimpia = numeroFactura.Trim();
            return await _context.OrdenesFacturasMuelle
                .AnyAsync(o => o.NumeroFactura == facturaLimpia, cancellationToken);
        }
    }
}
