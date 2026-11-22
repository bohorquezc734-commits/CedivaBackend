using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Dominio.Interfaces;
using Cediva.Domain.Agregados.Inventario;
using Cediva.Domain.Enumeraciones;
using Cediva.Dominio.ObjetosValor;
using Cediva.Domain.Excepciones;

namespace Cediva.Application.Caracteristicas.Inventarios.Comandos.RegistrarMovimientos
{
    public record RegistrarMovimientoComando(Guid ProductoId, decimal CantidadValor, TipoMovimiento Tipo) : IRequest<Guid>;

    public class RegistrarMovimientoHandler : IRequestHandler<RegistrarMovimientoComando, Guid>
    {
        private readonly IRepositorioInventario _repositorioInventario;
        private readonly IRepositorioProducto _repositorioProducto;

        public RegistrarMovimientoHandler(IRepositorioInventario repositorioInventario, IRepositorioProducto repositorioProducto)
        {
            _repositorioInventario = repositorioInventario;
            _repositorioProducto = repositorioProducto;
        }

        public async Task<Guid> Handle(RegistrarMovimientoComando request, CancellationToken cancellationToken)
        {
            var producto = await _repositorioProducto.ObtenerPorId(request.ProductoId, cancellationToken);
            if (producto == null) throw new ReglaDeNegocioException("El producto no existe en el catálogo.");

            var inventario = await _repositorioInventario.ObtenerPorProducto(request.ProductoId, cancellationToken);

            if (inventario == null)
            {
                if (request.Tipo == TipoMovimiento.Salida)
                    throw new ReglaDeNegocioException("No se puede dar salida a un producto sin inventario previo.");

                inventario = new Inventario(
                    Guid.NewGuid(), 
                    request.ProductoId,
                    new Cantidad(request.CantidadValor, producto.UnidadMedida)
                );

                var movimiento = new Movimiento(
                    Guid.Empty,
                    request.Tipo,
                    new Cantidad(request.CantidadValor, producto.UnidadMedida),
                    inventario.Id
                );

                inventario.AgregarMovimiento(movimiento);
                await _repositorioInventario.Agregar(inventario, cancellationToken);
            }
            else
            {
                decimal nuevaCantidad = request.Tipo == TipoMovimiento.Entrada
                    ? inventario.CantidadDisponible.Valor + request.CantidadValor
                    : inventario.CantidadDisponible.Valor - request.CantidadValor;

                if (nuevaCantidad < 0)
                    throw new ReglaDeNegocioException($"Stock insuficiente. Stock actual: {inventario.CantidadDisponible.Valor}");

                inventario.ActualizarCantidad(new Cantidad(nuevaCantidad, inventario.CantidadDisponible.Unidad));

                var movimiento = new Movimiento(
                    Guid.Empty,
                    request.Tipo,
                    new Cantidad(request.CantidadValor, inventario.CantidadDisponible.Unidad),
                    inventario.Id
                );

                inventario.AgregarMovimiento(movimiento);
            }

            await _repositorioInventario.GuardarCambios(inventario, cancellationToken);

            return inventario.Id;
        }
    }
}
