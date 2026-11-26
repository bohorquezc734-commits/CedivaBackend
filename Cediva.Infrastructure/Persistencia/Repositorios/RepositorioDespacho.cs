using Cediva.Domain.Agregados.Despacho;
using Cediva.Domain.Enumeraciones;
using Cediva.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cediva.Infrastructure.Persistencia.Repositorios
{
    public class RepositorioDespacho : RepositorioBase<Despacho>, IRepositorioDespacho
    {
        public RepositorioDespacho(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Despacho>> ObtenerPorCliente(string clienteId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<Despacho>> ObtenerPorEstado(EstadoDespacho estado, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Despacho?> ObtenerPorNumeroGuia(string numeroGuia, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<Despacho>> ObtenerPorRangoFechas(DateTime fechaInicio, DateTime fechaFin, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Despacho>> ObtenerTodosConItems(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Despacho>()
                .Include(d => d.Items) 
                .OrderByDescending(d => d.FechaDespacho) 
                .ToListAsync(cancellationToken);
        }
    }
}
