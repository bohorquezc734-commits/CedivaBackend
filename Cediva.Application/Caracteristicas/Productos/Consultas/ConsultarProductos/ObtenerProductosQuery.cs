using Cediva.Application.DTOs;
using Cediva.Dominio.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Cediva.Application.Caracteristicas.Productos.Consultas.ConsultarProductos
{
    public record ConsultarProductosQuery() : IRequest<List<ProductoDto>>;

    public class ConsultarProductosHandler : IRequestHandler<ConsultarProductosQuery, List<ProductoDto>>
    {
        private readonly IRepositorioProducto _repositorio;

        public ConsultarProductosHandler(IRepositorioProducto repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<ProductoDto>> Handle(ConsultarProductosQuery request, CancellationToken cancellationToken)
        {
            var productos = await _repositorio.ObtenerTodos(cancellationToken);

            return productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                PrecioUnitario = p.PrecioUnitario,
                CodigoBarras = p.CodigoBarras.Valor
            }).ToList();
        }
    }
}