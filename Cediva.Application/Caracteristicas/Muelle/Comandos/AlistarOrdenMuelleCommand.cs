using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Application.Caracteristicas.Muelle.DTOs;
using Cediva.Domain.Excepciones;
using Cediva.Domain.Interfaces;
using MediatR;

namespace Cediva.Application.Caracteristicas.Muelle.Comandos
{
    public record AlistarOrdenMuelleCommand(
        Guid Id,
        string AlistadoPor,
        string? HoraAlistamiento,
        List<AlistarLineaDto>? LineasAlistadas = null
    ) : IRequest<bool>;

    public class AlistarOrdenMuelleHandler : IRequestHandler<AlistarOrdenMuelleCommand, bool>
    {
        private readonly IOrdenFacturaMuelleRepository _repository;

        public AlistarOrdenMuelleHandler(IOrdenFacturaMuelleRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AlistarOrdenMuelleCommand request, CancellationToken cancellationToken)
        {
            var orden = await _repository.ObtenerPorIdConLineas(request.Id, cancellationToken);
            if (orden == null)
                throw new ReglaDeNegocioException($"No se encontró la orden de muelle con Id '{request.Id}'.");

            var lineas = request.LineasAlistadas?.Select(l => (l.CodigoBarras, l.CantidadAlistada));

            orden.Alistar(request.AlistadoPor, request.HoraAlistamiento, lineas);

            await _repository.Actualizar(orden, cancellationToken);
            return true;
        }
    }
}
