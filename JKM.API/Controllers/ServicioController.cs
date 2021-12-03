using JKM.APPLICATION.Aggregates;
using JKM.APPLICATION.Commands.Servicio.RegisterServicio;
using JKM.APPLICATION.Commands.Servicio.UpdateEstadoServicio;
using JKM.APPLICATION.Commands.Servicio.UpdateServicio;
using JKM.APPLICATION.Queries.Servicio.GetServicioById;
using JKM.APPLICATION.Queries.Servicio.GetServicioPaginado;
using JKM.UTILITY.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JKM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ServicioController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ServicioController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [SwaggerOperation("Retorna los Servicios por pagina")]
        [SwaggerResponse(200, "Retorna los Servicios", typeof(IEnumerable<ServicioModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetServicioPaginado()
        {
            return Ok(await _mediator.Send(new GetServicioPaginadoQuery()));
        }

        [HttpGet(template: "{idServicio}")]
        [SwaggerOperation("Retorna un Servicio en base a su Id")]
        [SwaggerResponse(200, "Retorna el Servicio", typeof(ServicioModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetServicioById(int idServicio)
        {
            return Ok(await _mediator.Send(new GetServicioByIdQuery { IdServicio = idServicio }));
        }

        [HttpPost]
        [SwaggerOperation("Registrar un nuevo Servicio")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrió un error de validación", typeof(ErrorModel))]

        public async Task<IActionResult> RegisterServicio([FromBody] RegisterServicioCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(template: "{idServicio}")]
        [SwaggerOperation("Actualizar un Servicio en base a su ID")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateServicio(int idServicio, [FromBody] UpdateServicioCommand request)
        {
            request.IdServicio = idServicio;

            return Ok(await _mediator.Send(request));
        }


        [HttpPut(template: "{idServicio}/Estado")]
        [SwaggerOperation("Actualizar estado de un Servicio en base a su ID")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateEstadoServicio(int idServicio)
        {
            return Ok(await _mediator.Send(new UpdateEstadoServicioCommand { IdServicio = idServicio }));
        }

    }
}
