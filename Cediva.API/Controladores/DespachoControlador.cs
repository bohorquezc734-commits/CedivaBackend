using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cediva.Application.Caracteristicas.Despachos.Comandos;

namespace Cediva.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class DespachoControlador : ControllerBase
    {
        private readonly ISender _mediator;

        public DespachoControlador(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CrearDespacho([FromBody] CrearDespachoComando comando)
        {
            var despachoId = await _mediator.Send(comando);
            return Ok(new { Mensaje = "Despacho generado y stock descontado exitosamente", DespachoId = despachoId });
        }
    }
}