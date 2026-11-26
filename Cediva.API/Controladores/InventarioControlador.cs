using System.Linq;
using System.Threading.Tasks;
using Cediva.Application.Caracteristicas.Inventarios.Comandos.RegistrarMovimientos;
using Cediva.Dominio.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cediva.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InventarioControlador : ControllerBase
    {
        private readonly ISender _mediator;

        public InventarioControlador(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("movimiento")]
        [Authorize(Roles = "Administrador,Supervisor,AuditorBodega")]
        public async Task<IActionResult> RegistrarMovimiento([FromBody] RegistrarMovimientoComando comando)
        {
            var inventarioId = await _mediator.Send(comando);
            return Ok(new { Mensaje = "Movimiento registrado exitosamente", InventarioId = inventarioId });
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Supervisor,AuditorBodega")]
        public async Task<IActionResult> ObtenerInventarios([FromServices] IRepositorioInventario repositorio)
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
