using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Domain.Agregados.Muelle;
using Cediva.Dominio.Interfaces;

namespace Cediva.Domain.Interfaces
{
    public interface IOrdenFacturaMuelleRepository : IRepositorio<OrdenFacturaMuelle>
    {
        Task<OrdenFacturaMuelle?> ObtenerPorIdConLineas(Guid id, CancellationToken cancellationToken = default);
        Task<List<OrdenFacturaMuelle>> ObtenerTodosConLineas(CancellationToken cancellationToken = default);
        Task<bool> ExisteNumeroFactura(string numeroFactura, CancellationToken cancellationToken = default);
    }
}
