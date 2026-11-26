using System;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Domain.Excepciones;
using Cediva.Domain.Interfaces;
using MediatR;

namespace Cediva.Application.Caracteristicas.Muelle.Comandos
{
    public record DespacharOrdenMuelleCommand(
        Guid Id,
        string DespachadoPor,
        string? HoraDespacho,
        string Transportadora,
        string PlacaVehiculo,
        string Conductor
    ) : IRequest<bool>;

    public class DespacharOrdenMuelleHandler : IRequestHandler<DespacharOrdenMuelleCommand, bool>
    {
        private readonly IOrdenFacturaMuelleRepository _repository;

        public DespacharOrdenMuelleHandler(IOrdenFacturaMuelleRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DespacharOrdenMuelleCommand request, CancellationToken cancellationToken)
        {
            var orden = await _repository.ObtenerPorIdConLineas(request.Id, cancellationToken);
            if (orden == null)
                throw new ReglaDeNegocioException($"No se encontró la orden con Id '{request.Id}'.");

            orden.Despachar(
                request.DespachadoPor,
                request.HoraDespacho ?? DateTime.Now.ToString("HH:mm:ss"),
                request.Transportadora,
                request.PlacaVehiculo,
                request.Conductor
            );

            await _repository.Actualizar(orden, cancellationToken);
            return true;
        }
    }
}
