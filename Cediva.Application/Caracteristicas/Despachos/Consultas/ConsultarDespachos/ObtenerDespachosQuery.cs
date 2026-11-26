using MediatR;
using Cediva.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cediva.Application.Caracteristicas.Despachos.Consultas
{
    // DTO para enviar al frontend
    public record ItemDespachoHistorialDto(Guid ProductoId, int Cantidad);
    public record DespachoHistorialDto(Guid Id, string Cliente, DateTime Fecha, List<ItemDespachoHistorialDto> Items);

    public record ObtenerDespachosQuery() : IRequest<List<DespachoHistorialDto>>;

    public class ObtenerDespachosHandler : IRequestHandler<ObtenerDespachosQuery, List<DespachoHistorialDto>>
    {
        private readonly IRepositorioDespacho _repositorio;

        public ObtenerDespachosHandler(IRepositorioDespacho repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<DespachoHistorialDto>> Handle(ObtenerDespachosQuery request, CancellationToken cancellationToken)
        {
            var despachos = await _repositorio.ObtenerTodosConItems(cancellationToken);

            return despachos.Select(d => new DespachoHistorialDto(
                d.Id,
                d.ClienteNombre,
                d.FechaDespacho,
                d.Items.Select(i => new ItemDespachoHistorialDto(i.ProductoId, (int)i.Cantidad.Valor)).ToList()
            )).ToList();
        }
    }
}