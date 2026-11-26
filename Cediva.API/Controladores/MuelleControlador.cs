using System;
using System.Threading.Tasks;
using Cediva.Application.Caracteristicas.Muelle.Comandos;
using Cediva.Application.Caracteristicas.Muelle.Consultas;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cediva.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MuelleControlador : ControllerBase
    {
        private readonly ISender _mediator;

        public MuelleControlador(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Supervisor,AuditorBodega")]
        public async Task<IActionResult> ObtenerOrdenes()
        {
            var ordenes = await _mediator.Send(new ObtenerOrdenesMuelleQuery());
            return Ok(ordenes);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Supervisor")]
        public async Task<IActionResult> CrearOrden([FromBody] CrearOrdenMuelleCommand comando)
        {
            var id = await _mediator.Send(comando);
            return Ok(new { Mensaje = "Orden de muelle creada exitosamente", Id = id });
        }

        [HttpPut("{id}/auditar")]
        [Authorize(Roles = "Administrador,Supervisor,AuditorBodega")]
        public async Task<IActionResult> AuditarOrden(Guid id, [FromBody] AuditarOrdenMuelleCommand comando)
        {
            var comandoConId = comando with { Id = id };
            var resultado = await _mediator.Send(comandoConId);
            return Ok(new { Mensaje = "Orden de muelle auditada exitosamente", Exito = resultado });
        }

        [HttpPut("{id}/despachar")]
        [Authorize(Roles = "Administrador,Supervisor,AuditorBodega")]
        public async Task<IActionResult> DespacharOrden(Guid id, [FromBody] DespacharOrdenMuelleCommand comando)
        {
            var comandoConId = comando with { Id = id };
            var resultado = await _mediator.Send(comandoConId);
            return Ok(new { Mensaje = "Orden de muelle despachada exitosamente", Exito = resultado });
        }
    }
}
