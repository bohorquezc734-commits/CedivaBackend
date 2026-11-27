using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Application.Caracteristicas.Muelle.DTOs;
using Cediva.Domain.Constantes;
using Cediva.Domain.Excepciones;
using Cediva.Domain.Interfaces;
using MediatR;

namespace Cediva.Application.Caracteristicas.Muelle.Comandos
{
    public record AuditarOrdenMuelleCommand(
        Guid Id,
        string AuditadoPor,
        string? HoraAuditoria,
        List<AuditarLineaDto> LineasPistoleadas
    ) : IRequest<bool>;

    public class AuditarOrdenMuelleHandler : IRequestHandler<AuditarOrdenMuelleCommand, bool>
    {
        private readonly IOrdenFacturaMuelleRepository _repository;

        public AuditarOrdenMuelleHandler(IOrdenFacturaMuelleRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AuditarOrdenMuelleCommand request, CancellationToken cancellationToken)
        {
            var orden = await _repository.ObtenerPorIdConLineas(request.Id, cancellationToken);
            if (orden == null)
                throw new ReglaDeNegocioException($"No se encontró la orden con Id '{request.Id}'.");

            var pistoleadas = (request.LineasPistoleadas ?? new List<AuditarLineaDto>())
                .Select(l => (l.CodigoBarras, l.CantidadPistoleada));

            orden.Auditar(request.AuditadoPor, request.HoraAuditoria ?? DateTime.Now.ToString(FormatosLogistica.HoraEstandar), pistoleadas);

            await _repository.Actualizar(orden, cancellationToken);
            return true;
        }
    }
}
