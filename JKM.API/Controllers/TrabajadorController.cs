using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JKM.APPLICATION.Queries.Trabajador.GetTrabajadoresPaginado;
using JKM.APPLICATION.Queries.Trabajador.GetTipoTrabajador;
using JKM.APPLICATION.Queries.Trabajador.GetTrabajadorByEstado;
using JKM.APPLICATION.Queries.Trabajador.GetTrabajadorById;
using JKM.APPLICATION.Queries.Trabajador.GetEstadoTrabajador;
using JKM.UTILITY.Utils;
using JKM.APPLICATION.Aggregates;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using JKM.APPLICATION.Commands.Trabajador.RegisterTrabajador;
using JKM.APPLICATION.Commands.Trabajador.UpdateTrabajador;

namespace JKM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrabajadorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation("Trae los trabajadores por página")]
        [SwaggerResponse(200, "Retorna los trabajadores", typeof(PaginadoResponse<TrabajadorModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTrabajadorPaginado([FromQuery] GetTrabajadoresPaginadoQuery request)
        {
            try
            {
                var response = await _mediator.Send(request);
                if (response == null) return NotFound(new { msg = "Error al traer los resultados" });
                return Ok(response);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost]
        [SwaggerOperation("Registra un nuevo trabajador")]
        [SwaggerResponse(200, "Registra un trabajador", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RegisterTrabajador([FromBody] RegisterTrabajadorCommand request)
        {
            try
            {
                return Ok(await _mediator.Send(request));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPut(template: "{idTrabajador}")]
        [SwaggerOperation("Actualiza un trabajador en basea su id")]
        [SwaggerResponse(200, "Actualiza un trabajador", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateTrabajador(int idTrabajador, [FromBody] UpdateTrabajadorCommand request)
        {
            try
            {
                request.IdTrabajador = idTrabajador;
                return Ok(await _mediator.Send(request));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet(template: "{idTrabajador}")]
        [SwaggerOperation("Retorna un trabajador en base a su Id")]
        [SwaggerResponse(200, "Retorna el trabajador", typeof(TrabajadorModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTrabajadorById(int idTrabajador)
        {
            try
            {
                return Ok(await _mediator.Send(new GetTrabajadorByIdQuery { IdTrabajador = idTrabajador }));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet(template: "Estado/{idEstado}")]
        [SwaggerOperation("Retorna los trabajadores en base al estado")]
        [SwaggerResponse(200, "Retorna los trabajadores", typeof(IEnumerable<TrabajadorModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTrabajadorByEstado(int idEstado)
        {
            try
            {
                return Ok(await _mediator.Send(new GetTrabajadorByEstadoQuery { IdEstado = idEstado }));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet(template: "Estado")]
        [SwaggerOperation("Retorna los estados de trabajadores")]
        [SwaggerResponse(200, "Retorna los estados", typeof(IEnumerable<Identifier>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetEstadoTrabajador()
        {
            try
            {
                var response = await _mediator.Send(new GetEstadoTrabajadorQuery());
                if (response == null) return NotFound(new { msg = "Error al traer los resultados" });
                return Ok(response);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        
        [HttpGet(template: "Tipo")]
        [SwaggerOperation("Retorna los tipos de trabajadores")]
        [SwaggerResponse(200, "Retorna los tipos", typeof(IEnumerable<TipoTrabajador>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTipoTrabajador()
        {
            GetTipoTrabajadorQuery request = new GetTipoTrabajadorQuery();
            try
            {
                return Ok(await _mediator.Send(request));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
