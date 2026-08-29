using Cediva.Dominio.Agregados.Producto;
using Cediva.Dominio.Excepciones;
using Cediva.Dominio.Interfaces;
using Cediva.Dominio.ObjetosValor;
using MediatR;

namespace Cediva.Application.Caracteristicas.Productos.Comandos.CrearProducto
{
    public class CrearProductoManejador : IRequestHandler<CrearProductoComando, Guid>
    {
        private readonly IRepositorioProducto _repositorioProducto;
        private readonly IRepositorioCategoria _repositorioCategoria;
        private readonly IRepositorioMarca _repositorioMarca;

        public CrearProductoManejador(
            IRepositorioProducto repositorioProducto,
            IRepositorioCategoria repositorioCategoria,
            IRepositorioMarca repositorioMarca)
        {
            _repositorioProducto = repositorioProducto;
            _repositorioCategoria = repositorioCategoria;
            _repositorioMarca = repositorioMarca;
        }

        public async Task<Guid> Handle(CrearProductoComando request, CancellationToken cancellationToken)
        {
            var existeCodigo = await _repositorioProducto.ExisteCodigoBarras(request.CodigoBarras, cancellationToken);
            if (existeCodigo)
                throw new ExcepcionDominio($"Ya existe un producto registrado con el código de barras {request.CodigoBarras}");

            var categoria = await _repositorioCategoria.ObtenerPorId(request.CategoriaId, cancellationToken);
            if (categoria == null)
                throw new ExcepcionDominio($"No se encontró la categoría con ID {request.CategoriaId}");

            Marca? marca = null;
            if (request.MarcaId.HasValue)
            {
                marca = await _repositorioMarca.ObtenerPorId(request.MarcaId.Value, cancellationToken);
                if (marca == null)
                    throw new ExcepcionDominio($"No se encontró la marca con ID {request.MarcaId.Value}");
            }

            var codigoBarras = new CodigoBarras(request.CodigoBarras);

            var producto = new Producto(
                request.Nombre,
                request.Descripcion,
                codigoBarras,
                categoria,
                request.PrecioUnitario,
                request.UnidadMedida,
                marca,
                request.ImagenUrl,
                request.PuntoReorden
            );

            await _repositorioProducto.Agregar(producto, cancellationToken);

            return producto.Id;
        }
    }
}
