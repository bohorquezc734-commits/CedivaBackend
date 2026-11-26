using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Application.Caracteristicas.Muelle.DTOs;
using Cediva.Domain.Agregados.Muelle;
using Cediva.Domain.Excepciones;
using Cediva.Domain.Interfaces;
using MediatR;

namespace Cediva.Application.Caracteristicas.Muelle.Comandos
{
    public record CrearOrdenMuelleCommand(
        string NumeroFactura,
        string ClienteNombre,
        string ClienteNit,
        string Muelle,
        List<CrearLineaOrdenDto> Lineas
    ) : IRequest<Guid>;

    public class CrearOrdenMuelleHandler : IRequestHandler<CrearOrdenMuelleCommand, Guid>
    {
        private readonly IOrdenFacturaMuelleRepository _repository;

        public CrearOrdenMuelleHandler(IOrdenFacturaMuelleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CrearOrdenMuelleCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.NumeroFactura))
                throw new ReglaDeNegocioException("El número de factura es obligatorio.");

            if (request.Lineas == null || request.Lineas.Count == 0)
                throw new ReglaDeNegocioException("La orden debe contener al menos una línea de producto.");

            var existe = await _repository.ExisteNumeroFactura(request.NumeroFactura, cancellationToken);
            if (existe)
                throw new ReglaDeNegocioException($"Ya existe una orden registrada con la factura '{request.NumeroFactura}'.");

            var orden = new OrdenFacturaMuelle(
                Guid.NewGuid(),
                request.NumeroFactura,
                request.ClienteNombre,
                request.ClienteNit,
                request.Muelle
            );

            foreach (var item in request.Lineas)
            {
                orden.AgregarLinea(item.CodigoBarras, item.CantidadPedida);
            }

            await _repository.Agregar(orden, cancellationToken);
            await _repository.GuardarCambios(orden, cancellationToken);

            return orden.Id;
        }
    }
}
