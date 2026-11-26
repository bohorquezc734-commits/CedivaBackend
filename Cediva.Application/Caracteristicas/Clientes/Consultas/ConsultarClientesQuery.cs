using MediatR;
using Cediva.Dominio.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cediva.Application.Caracteristicas.Clientes.Consultas
{
    public record ClienteDto(Guid Id, string Nombre, string NIT);
    public record ConsultarClientesQuery() : IRequest<List<ClienteDto>>;

    public class ConsultarClientesHandler : IRequestHandler<ConsultarClientesQuery, List<ClienteDto>>
    {
        private readonly IRepositorioCliente _repositorio;

        public ConsultarClientesHandler(IRepositorioCliente repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<ClienteDto>> Handle(ConsultarClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _repositorio.ObtenerTodos(cancellationToken);
            return clientes.Select(c => new ClienteDto(c.Id, c.Nombre, c.NIT)).ToList();
        }
    }
}