using Cediva.Application.Caracteristicas.Productos.Comandos.CrearProducto;
using Cediva.Application.Caracteristicas.Productos.Consultas.ConsultarProductos;
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
        [HttpGet]
        public async Task<IActionResult> ConsultarProductos()
        {
   
            var productos = await _mediator.Send(new ConsultarProductosQuery());
            return Ok(productos);
        }
    }
}