using System.Threading.Tasks;
using Cediva.Application.Caracteristicas.Despachos.Comandos;
using Cediva.Application.Caracteristicas.Despachos.Consultas;
using Cediva.Domain.Constantes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cediva.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DespachoControlador : ControllerBase
    {
        private readonly ISender _mediator;

        public DespachoControlador(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = RolesSistema.AdministracionYSupervision)]
        public async Task<IActionResult> CrearDespacho([FromBody] CrearDespachoComando comando)
        {
            var despachoId = await _mediator.Send(comando);
            return Ok(new { Mensaje = "Despacho generado y stock descontado exitosamente", DespachoId = despachoId });
        }

        [HttpGet]
        [Authorize(Roles = RolesSistema.AdministracionYSupervision)]
        public async Task<IActionResult> ObtenerHistorial()
        {
            var despachos = await _mediator.Send(new ObtenerDespachosQuery());
            return Ok(despachos);
        }
    }
}
