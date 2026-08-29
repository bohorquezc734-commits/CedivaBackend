using Cediva.Domain.Enumeraciones;
using MediatR;

namespace Cediva.Application.Caracteristicas.Productos.Comandos.CrearProducto
{
    public record CrearProductoComando(
        string Nombre,
        string? Descripcion,
        string CodigoBarras,
        Guid CategoriaId,
        decimal PrecioUnitario,
        UnidadMedida UnidadMedida,
        Guid? MarcaId = null,
        string? ImagenUrl = null,
        int PuntoReorden = 10
    ) : IRequest<Guid>;
}
