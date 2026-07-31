using Cediva.Domain.Agregados.Despacho;
using Cediva.Domain.Enumeraciones;
using Cediva.Dominio.Interfaces;

namespace Cediva.Infrastructure.Persistencia.Repositorios
{
    public class RepositorioDespacho : RepositorioBase<Despacho>, IRepositorioDespacho
    {
        public RepositorioDespacho(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Despacho>> ObtenerPorCliente(string clienteId, CancellationToken cancellationToken = default)
        {
            // TODO: Implement when Despacho properties are defined
            throw new NotImplementedException();
        }

        public Task<List<Despacho>> ObtenerPorEstado(EstadoDespacho estado, CancellationToken cancellationToken = default)
        {
            // TODO: Implement when Despacho properties are defined
            throw new NotImplementedException();
        }

        public Task<Despacho?> ObtenerPorNumeroGuia(string numeroGuia, CancellationToken cancellationToken = default)
        {
            // TODO: Implement when Despacho properties are defined
            throw new NotImplementedException();
        }

        public Task<List<Despacho>> ObtenerPorRangoFechas(DateTime fechaInicio, DateTime fechaFin, CancellationToken cancellationToken = default)
        {
            // TODO: Implement when Despacho properties are defined
            throw new NotImplementedException();
        }
    }
}
