using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JKM.APPLICATION.Queries.Trabajador.GetTrabajadoresPaginado;
using JKM.APPLICATION.Queries.Trabajador.GetTipoTrabajador;
using JKM.APPLICATION.Queries.Trabajador.GetTrabajadorByEstado;
using JKM.APPLICATION.Queries.Trabajador.GetTrabajadorById;
using JKM.APPLICATION.Queries.Trabajador.GetEstadoTrabajador;
using JKM.APPLICATION.Queries.Trabajador.GetTrabajadorDisponibleProyecto;
using JKM.UTILITY.Utils;
using JKM.APPLICATION.Aggregates;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using JKM.APPLICATION.Commands.Trabajador.RegisterTrabajador;
using JKM.APPLICATION.Commands.Trabajador.UpdateTrabajador;
using JKM.APPLICATION.Commands.Trabajador.DeleteTrabajador;
using JKM.APPLICATION.Queries.Trabajador.GetTipoTrabajadorById;
using JKM.APPLICATION.Commands.Trabajador.UpdateTipoTrabajador;
using JKM.APPLICATION.Commands.Trabajador.RegisterTipoTrabajador;

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
        public async Task<IActionResult> GetTrabajadorPaginado()
        {
            var response = await _mediator.Send(new GetTrabajadoresPaginadoQuery());
            if (response == null) return NotFound(new { msg = "Error al traer los resultados" });
            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation("Registra un nuevo trabajador")]
        [SwaggerResponse(200, "Registra un trabajador", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RegisterTrabajador([FromBody] RegisterTrabajadorCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(template: "{idTrabajador}")]
        [SwaggerOperation("Actualiza un trabajador en basea su id")]
        [SwaggerResponse(200, "Actualiza un trabajador", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateTrabajador(int idTrabajador, [FromBody] UpdateTrabajadorCommand request)
        {
            request.IdTrabajador = idTrabajador;
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete(template: "{idTrabajador}")]
        [SwaggerOperation("Elimina un trabajador en basea su id")]
        [SwaggerResponse(200, "Elimina un trabajador", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> DeleteTranbajador(int idTrabajador)
        {
            DeleteTrabajadorCommand request = new DeleteTrabajadorCommand() { IdTrabajador = idTrabajador };
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(template: "{idTrabajador}")]
        [SwaggerOperation("Retorna un trabajador en base a su Id")]
        [SwaggerResponse(200, "Retorna el trabajador", typeof(TrabajadorModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTrabajadorById(int idTrabajador)
        {
            return Ok(await _mediator.Send(new GetTrabajadorByIdQuery { IdTrabajador = idTrabajador }));
        }

        [HttpGet(template: "Estado/{idEstado}")]
        [SwaggerOperation("Retorna los trabajadores en base al estado")]
        [SwaggerResponse(200, "Retorna los trabajadores", typeof(IEnumerable<TrabajadorModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTrabajadorByEstado(int idEstado)
        {
            return Ok(await _mediator.Send(new GetTrabajadorByEstadoQuery { IdEstado = idEstado }));
        }

        [HttpGet(template: "Estado")]
        [SwaggerOperation("Retorna los estados de trabajadores")]
        [SwaggerResponse(200, "Retorna los estados", typeof(IEnumerable<Identifier>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetEstadoTrabajador()
        {
            var response = await _mediator.Send(new GetEstadoTrabajadorQuery());
            if (response == null) return NotFound(new { msg = "Error al traer los resultados" });
            return Ok(response);
        }

        [HttpGet(template: "Tipo")]
        [SwaggerOperation("Retorna los tipos de trabajadores")]
        [SwaggerResponse(200, "Retorna los tipos", typeof(IEnumerable<TipoTrabajador>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTipoTrabajador()
        {
            GetTipoTrabajadorQuery request = new GetTipoTrabajadorQuery();
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(template: "Tipo/{idTipoTrabajador}")]
        [SwaggerOperation("Retorna un tipo en base a su Id")]
        [SwaggerResponse(200, "Retorna el tipo", typeof(IEnumerable<TipoTrabajador>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTipoTrabajadorById(int idTipoTrabajador)
        {
            GetTipoTrabajadorByIdQuery request = new GetTipoTrabajadorByIdQuery();
            request.IdTrabajador = idTipoTrabajador;
            return Ok(await _mediator.Send(request));
        }

        [HttpPost(template: "Tipo")]
        [SwaggerOperation("Registra un nuevo tipo trabajador")]
        [SwaggerResponse(200, "Registra un tipo trabajador", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RegisterTipoTrabajador([FromBody] RegisterTipoTrabajadorCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(template: "Tipo/{idTipoTrabajador}")]
        [SwaggerOperation("Actualiza un tipo trabajador en basea su id")]
        [SwaggerResponse(200, "Actualiza un tipo trabajador", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateTipoTrabajador(int idTipoTrabajador, [FromBody] UpdateTipoTrabajadorCommand request)
        {
            request.IdTipoTrabajador = idTipoTrabajador;
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(template: "Disponible")]
        [SwaggerOperation("Retorna los trabajadores disponibles")]
        [SwaggerResponse(200, "Retorna los trabajadores", typeof(IEnumerable<TrabajadorModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTrabajadorDisponible()
        {
            return Ok(await _mediator.Send(new GetTrabajadorDisponibleProyectoQuery()));
        }
    }
}
