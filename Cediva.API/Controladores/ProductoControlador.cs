using Cediva.Application.Caracteristicas.Productos.Comandos.CrearProducto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cediva.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoControlador : ControllerBase
    {
        private readonly ISender _mediator;

        public ProductoControlador(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] CrearProductoComando comando)
        {
            var resultado = await _mediator.Send(comando);

            return Ok(new
            {
                Mensaje = "Producto procesado exitosamente",
                Data = resultado
            });
        }
    }
}