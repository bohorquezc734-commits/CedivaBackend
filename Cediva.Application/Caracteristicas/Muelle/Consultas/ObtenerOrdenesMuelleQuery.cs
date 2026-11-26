using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Application.Caracteristicas.Muelle.DTOs;
using Cediva.Domain.Interfaces;
using MediatR;

namespace Cediva.Application.Caracteristicas.Muelle.Consultas
{
    public record ObtenerOrdenesMuelleQuery() : IRequest<List<OrdenFacturaMuelleDto>>;

    public class ObtenerOrdenesMuelleHandler : IRequestHandler<ObtenerOrdenesMuelleQuery, List<OrdenFacturaMuelleDto>>
    {
        private readonly IOrdenFacturaMuelleRepository _repository;

        public ObtenerOrdenesMuelleHandler(IOrdenFacturaMuelleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<OrdenFacturaMuelleDto>> Handle(ObtenerOrdenesMuelleQuery request, CancellationToken cancellationToken)
        {
            var ordenes = await _repository.ObtenerTodosConLineas(cancellationToken);

            return ordenes
                .OrderByDescending(o => o.FechaCreacion)
                .Select(o => new OrdenFacturaMuelleDto(
                    o.Id,
                    o.NumeroFactura,
                    o.ClienteNombre,
                    o.ClienteNit,
                    o.Muelle,
                    o.Estado,
                    o.FechaCreacion,
                    o.AuditadoPor,
                    o.HoraAuditoria,
                    o.DespachadoPor,
                    o.HoraDespacho,
                    o.Transportadora,
                    o.PlacaVehiculo,
                    o.Conductor,
                    o.Lineas.Select(l => new LineaOrdenMuelleDto(
                        l.Id,
                        l.CodigoBarras,
                        l.CantidadPedida,
                        l.CantidadPistoleada
                    )).ToList()
                )).ToList();
        }
    }
}
