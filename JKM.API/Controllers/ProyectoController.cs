using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JKM.APPLICATION.Queries.Proyecto.GetActividadesByProyecto;
using JKM.APPLICATION.Queries.Proyecto.GetEstadosProyecto;
using JKM.APPLICATION.Queries.Proyecto.GetProyectoById;
using JKM.APPLICATION.Queries.Proyecto.GetProyectoPaginado;
using JKM.APPLICATION.Queries.Proyecto.GetTrabajadoresByProyecto;
using JKM.APPLICATION.Commands.Proyecto.RegisterProyecto;
using JKM.APPLICATION.Commands.Proyecto.UpdateProyecto;
using JKM.APPLICATION.Commands.Proyecto.UpdateActividadByProyecto;
using JKM.APPLICATION.Commands.Proyecto.DeleteTrabajadorByProyecto;
using JKM.APPLICATION.Commands.Proyecto.RegisterTrabajadorByProyecto;
using JKM.UTILITY.Utils;
using JKM.APPLICATION.Aggregates;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProyectoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation("Retorna los proyectos por página")]
        [SwaggerResponse(200, "Retorna los proyectos", typeof(PaginadoResponse<ProyectoModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetProyectoPaginado([FromQuery] GetProyectoPaginadoQuery request)
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

        [HttpGet(template: "{idProyecto}")]
        [SwaggerOperation("Retorna un proyecto en base a su Id")]
        [SwaggerResponse(200, "Retorna un proyecto", typeof(ProyectoModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetProyectoById(int idProyecto)
        {
            try
            {
                return Ok(await _mediator.Send(new GetProyectoByIdQuery { IdProyecto = idProyecto }));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet(template: "Estado")]
        [SwaggerOperation("Retorna los estados de los proyectos")]
        [SwaggerResponse(200, "Retorna los estados", typeof(ProyectoModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetEstadosProyecto()
        {
            GetEstadosProyectoQuery request = new GetEstadosProyectoQuery();
            try
            {
                return Ok(await _mediator.Send(request));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost()]
        [SwaggerOperation("Registra un nuevo proyecto")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RegisterProyecto([FromBody] RegisterProyectoCommand request)
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

        [HttpPut(template: "{idProyecto}")]
        [SwaggerOperation("Actualiza un proyecto en base a su Id")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateProyecto(int idProyecto, [FromBody] UpdateProyectoCommand request)
        {
            try
            {
                request.IdProyecto = idProyecto;
                return Ok(await _mediator.Send(request));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet(template: "{idProyecto}/Trabajadores")]
        [SwaggerOperation("Retorna a los trabajadores para un proyecto")]
        [SwaggerResponse(200, "Retorna los trabajador y sus cantidades", typeof(TrabajadorProyectoModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTrabajadoresByProyecto(int idProyecto)
        {
            try
            {
                return Ok(await _mediator.Send(new GetTrabajadoresByProyectoQuery { IdProyecto = idProyecto}));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPost(template: "{idProyecto}/Trabajadores")]
        [SwaggerOperation("Asigna un trabajador y su precio a un proyecto")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RegisterTrabajadorByProyecto(int idProyecto, [FromBody] RegisterTrabajadorByProyectoCommand request)
        {
            try
            {
                request.IdProyecto = idProyecto;
                return Ok(await _mediator.Send(request));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpDelete(template: "{idProyecto}/Trabajadores/{idTrabajador}")]
        [SwaggerOperation("Elimina el trabajador del proyecto en base a sus id's")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> DeleteTrabajadorByProyecto(int idProyecto, int idTrabajador)
        {
            try
            {
                DeleteTrabajadorByProyectoCommand request = new DeleteTrabajadorByProyectoCommand
                {
                    IdProyecto = idProyecto,
                    IdTrabajador = idTrabajador
                };
                return Ok(await _mediator.Send(request));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet(template: "{idProyecto}/Actividades")]
        [SwaggerOperation("Retorna las actividades de un proyecto")]
        [SwaggerResponse(200, "Retorna las actividades", typeof(IEnumerable<ActividadCotizacionModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetActividadesByProyecto(int idProyecto)
        {
            try
            {
                return Ok(await _mediator.Send(new GetActividadesByProyectoQuery { IdProyecto = idProyecto}));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPut(template: "{idProyecto}/Actividades/{idActividad}")]
        [SwaggerOperation("Actualiza una actividad de un proyecto")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateActividadByProyecto(int idProyecto, int idActividad, [FromBody] UpdateActividadByProyectoCommand request)
        {
            try
            {
                request.IdProyecto = idProyecto;
                request.IdActividad = idActividad;
                return Ok(await _mediator.Send(request));
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
