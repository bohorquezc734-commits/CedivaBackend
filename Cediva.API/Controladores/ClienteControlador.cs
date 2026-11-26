using System.Threading.Tasks;
using Cediva.Application.Caracteristicas.Clientes.Comandos;
using Cediva.Application.Caracteristicas.Clientes.Consultas;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cediva.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClienteControlador : ControllerBase
    {
        private readonly ISender _mediator;

        public ClienteControlador(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Supervisor")]
        public async Task<IActionResult> Crear([FromBody] CrearClienteComando comando)
        {
            var id = await _mediator.Send(comando);
            return Ok(new { Mensaje = "Cliente creado exitosamente", Id = id });
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Supervisor")]
        public async Task<IActionResult> ConsultarClientes()
        {
            var clientes = await _mediator.Send(new ConsultarClientesQuery());
            return Ok(clientes);
        }
    }
}
