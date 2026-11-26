using System.Threading.Tasks;
using Cediva.Application.Caracteristicas.Autenticacion.Comandos.Login;
using Cediva.Application.Caracteristicas.Autenticacion.Comandos.RegistrarUsuario;
using Cediva.Application.Caracteristicas.Autenticacion.Consultas.ObtenerUsuarios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cediva.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthControlador : ControllerBase
    {
        private readonly ISender _mediator;

        public AuthControlador(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginComando comando)
        {
            var respuesta = await _mediator.Send(comando);
            return Ok(respuesta);
        }

        [HttpPost("registrar")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioComando comando)
        {
            var usuarioId = await _mediator.Send(comando);
            return Ok(new
            {
                Mensaje = "Usuario registrado exitosamente",
                UsuarioId = usuarioId
            });
        }

        [HttpGet("usuarios")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _mediator.Send(new ObtenerUsuariosQuery());
            return Ok(usuarios);
        }
    }
}
