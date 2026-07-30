using Cediva.Domain.Agregados.Despacho;
using Cediva.Domain.Enumeraciones;


namespace Cediva.Dominio.Interfaces;

public interface IRepositorioDespacho : IRepositorio<Despacho>
{
    Task<List<Despacho>> ObtenerPorEstado(EstadoDespacho estado, CancellationToken cancellationToken = default);
    Task<List<Despacho>> ObtenerPorCliente(string clienteId, CancellationToken cancellationToken = default);
    Task<List<Despacho>> ObtenerPorRangoFechas(DateTime fechaInicio, DateTime fechaFin, CancellationToken cancellationToken = default);
    Task<Despacho?> ObtenerPorNumeroGuia(string numeroGuia, CancellationToken cancellationToken = default);
}