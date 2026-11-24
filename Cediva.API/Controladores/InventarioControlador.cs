using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cediva.Application.Caracteristicas.Inventarios.Comandos.RegistrarMovimientos;

namespace Cediva.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioControlador : ControllerBase
    {
        private readonly ISender _mediator;

        public InventarioControlador(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("movimiento")]
        public async Task<IActionResult> RegistrarMovimiento([FromBody] RegistrarMovimientoComando comando)
        {
            var inventarioId = await _mediator.Send(comando);
            return Ok(new { Mensaje = "Movimiento registrado exitosamente", InventarioId = inventarioId });
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerInventarios([FromServices] Cediva.Dominio.Interfaces.IRepositorioInventario repositorio)
        {
            
            var inventarios = await repositorio.ObtenerTodos();

            var resultado = inventarios.Select(i => new
            {
                ProductoId = i.ProductoId,
                Stock = i.CantidadDisponible.Valor
            });

            return Ok(resultado);
        }
    }


}
