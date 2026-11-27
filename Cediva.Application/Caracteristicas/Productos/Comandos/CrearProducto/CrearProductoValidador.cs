using Cediva.Domain.Constantes;
using FluentValidation;

namespace Cediva.Application.Caracteristicas.Productos.Comandos.CrearProducto
{
    public class CrearProductoValidador : AbstractValidator<CrearProductoComando>
    {
        public CrearProductoValidador()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MinimumLength(LimitesDominio.Catalogo.LongitudMinimaNombreProducto).WithMessage($"El nombre debe tener al menos {LimitesDominio.Catalogo.LongitudMinimaNombreProducto} caracteres.")
                .MaximumLength(LimitesDominio.Catalogo.LongitudMaximaNombreProducto).WithMessage($"El nombre no puede exceder los {LimitesDominio.Catalogo.LongitudMaximaNombreProducto} caracteres.");

            RuleFor(x => x.CodigoBarras)
                .NotEmpty().WithMessage("El código de barras es obligatorio.");

            RuleFor(x => x.CategoriaId)
                .NotEmpty().WithMessage("La categoría es obligatoria.");

            RuleFor(x => x.PrecioUnitario)
                .GreaterThanOrEqualTo(0).WithMessage("El precio unitario no puede ser negativo.")
                .LessThanOrEqualTo(99999999.99m).WithMessage("El precio unitario no puede superar 99,999,999.99");
            
            RuleFor(x => x.UnidadMedida)
                .IsInEnum().WithMessage("La unidad de medida seleccionada no es válida.");

            RuleFor(x => x.PuntoReorden)
                .GreaterThanOrEqualTo(0).WithMessage("El punto de reorden no puede ser negativo.");
        }
    }
}
