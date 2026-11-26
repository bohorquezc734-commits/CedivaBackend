using MediatR;
using Cediva.Domain.Agregados.Cliente;
using Cediva.Dominio.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cediva.Application.Caracteristicas.Clientes.Comandos
{
    public record CrearClienteComando(string Nombre, string NIT) : IRequest<Guid>;

    public class CrearClienteHandler : IRequestHandler<CrearClienteComando, Guid>
    {
        private readonly IRepositorioCliente _repositorio;

        public CrearClienteHandler(IRepositorioCliente repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Guid> Handle(CrearClienteComando request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente(Guid.NewGuid(), request.Nombre, request.NIT);
            await _repositorio.Agregar(cliente, cancellationToken);
            await _repositorio.GuardarCambios(cliente, cancellationToken);
            return cliente.Id;
        }
    }
}