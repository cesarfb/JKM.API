using MediatR;
using Microsoft.AspNetCore.Mvc;
using JKM.APPLICATION.Queries.Cotizacion.GetCotizacionPaginado;
using JKM.APPLICATION.Queries.Cotizacion.GetActividadesByCotizacion;
using JKM.APPLICATION.Queries.Cotizacion.GetEstadoCotizacion;
using JKM.APPLICATION.Queries.Cotizacion.GetTrabajadoresByCotizacion;
using JKM.APPLICATION.Queries.Cotizacion.GetCotizacionById;
using JKM.APPLICATION.Queries.Cotizacion.GetTipoCotizacion;
using JKM.APPLICATION.Queries.Cotizacion.GetDetalleOrdenByCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.AceptarCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.RechazarCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.RegisterCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.RegisterActividadCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.RegisterDetalleOrdenCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.UpdateCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.RegisterTrabajadorCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.UpdateTrabajadorCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.DeleteTrabajadorCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.DeleteActividadCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.UpdateActividadCotizacion;
using JKM.APPLICATION.Aggregates;
using System.Threading.Tasks;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using JKM.UTILITY.Utils;
using Microsoft.AspNetCore.Authorization;
using JKM.APPLICATION.Commands.Cotizacion.UpdateDetalleOrdenCotizacion;
using JKM.APPLICATION.Commands.Cotizacion.DeleteDetalleOrdenCotizacion;

namespace JKM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CotizacionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation("Retorna todas las cotizaciones")]
        [SwaggerResponse(200, "Retorna las cotizaciones", typeof(PaginadoResponse<CotizacionModel>))]
        [SwaggerResponse(204, "No se encontraron cotizaciones")]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetCotizacion([FromQuery] GetCotizacion request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(template: "{idCotizacion}")]
        [SwaggerOperation("Retorna una cotizacion en base a su Id")]
        [SwaggerResponse(200, "Retorna la cotizacion", typeof(CotizacionModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetCotizacionById(int idCotizacion)
        {
            return Ok(await _mediator.Send(new GetCotizacionByIdQuery { IdCotizacion = idCotizacion }));
        }

        [HttpGet(template: "Estado")]
        [SwaggerOperation("Retorna los estados de las cotizaciones")]
        [SwaggerResponse(200, "Retorna los estados", typeof(IEnumerable<Identifier>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetEstadoCotizacion()
        {
            return Ok(await _mediator.Send(new GetEstadoCotizacionQuery()));
        }

        [HttpPut(template: "{idCotizacion}/Aceptar")]
        [SwaggerOperation("Acepta la cotizacion")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> Aceptar(int idCotizacion)
        {
            return Ok(await _mediator.Send(new AceptarCotizacionCommand() { IdCotizacion = idCotizacion }));
        }

        [HttpPut(template: "{idCotizacion}/Rechazar")]
        [SwaggerOperation("Rechaza la cotizacion")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RechazarCotizacion(int idCotizacion)
        {
            return Ok(await _mediator.Send(new RechazarCotizacionCommand { IdCotizacion = idCotizacion }));
        }

        [HttpPost]
        [SwaggerOperation("Registra una nueva cotizacion")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RegisterCotizacion([FromBody] RegisterCotizacionCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(template: "{idCotizacion}")]
        [SwaggerOperation("Actualiza una cotizacion en base a su Id")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateCotizacion(int idCotizacion, [FromBody] UpdateCotizacionCommand request)
        {
            request.IdCotizacion = idCotizacion;
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(template: "{idCotizacion}/Trabajadores")]
        [SwaggerOperation("Retorna a los tipos de trabajadores para una cotizacion")]
        [SwaggerResponse(200, "Retorna los tipos de trabajador y sus cantidades", typeof(IEnumerable<TipoTrabajadorModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTrabajadoresByCotizacion(int idCotizacion)
        {
            return Ok(await _mediator.Send(new GetTrabajadoresByCotizacionQuery { IdCotizacion = idCotizacion }));
        }

        [HttpPost(template: "{idCotizacion}/Trabajadores")]
        [SwaggerOperation("Asigna un tipo de trabajador y su cantidad a una cotizacion")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RegisterTrabajadorCotizacion(int idCotizacion, [FromBody] RegisterTrabajadorCotizacionCommand request)
        {
            request.IdCotizacion = idCotizacion;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(template: "{idCotizacion}/Trabajadores/{idTipoTrabajador}")]
        [SwaggerOperation("Actualiza el tipo de trabajador y su cantidad de una cotizacion")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateTrabajadorCotizacion(int idCotizacion, int idTipoTrabajador, [FromBody] UpdateTrabajadorCotizacionCommand request)
        {
            request.IdCotizacion = idCotizacion;
            request.IdTipoTrabajador = idTipoTrabajador;
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete(template: "{idCotizacion}/Trabajadores/{idTipo}")]
        [SwaggerOperation("Elimina el tipo de trabajador y su cantidad de una cotizacion en base a su id")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> DeleteTrabajador(int idCotizacion, int idTipo)
        {
            return Ok(await _mediator.Send(new DeleteTrabajadorCotizacionCommand
            {
                IdCotizacion = idCotizacion,
                IdTipoTrabajador = idTipo
            }));
        }

        [HttpGet(template: "{idCotizacion}/Actividades")]
        [SwaggerOperation("Retorna las actividades de una cotizacion")]
        [SwaggerResponse(200, "Retorna las actividades", typeof(IEnumerable<ActividadCotizancionTreeNode>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetActividadesByCotizacion(int idCotizacion)
        {
            return Ok(await _mediator.Send(new GetActividadesByCotizacionQuery { IdCotizacion = idCotizacion }));
        }

        [HttpPost(template: "{idCotizacion}/Actividades")]
        [SwaggerOperation("Registra una nueva actividad a una cotizacion")]
        [SwaggerResponse(200, "Retorna las actividades", typeof(IEnumerable<ActividadCotizacionModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RegisterActividadCotizacion(int idCotizacion, [FromBody] RegisterActividadCotizacionCommand request)
        {
            request.IdCotizacion = idCotizacion;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(template: "{idCotizacion}/Actividades/{idActividad}")]
        [SwaggerOperation("Actualiza una actividad de una cotizacion")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateActividadCotizacion(int idCotizacion, int idActividad, [FromBody] UpdateActividadCotizacionCommand request)
        {
            request.IdCotizacion = idCotizacion;
            request.IdActividad = idActividad;
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete(template: "{idCotizacion}/Actividades/{idActividad}")]
        [SwaggerOperation("Elimina una actividad de la cotizacion en base a su idActividad")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> DeleteActividad(int idCotizacion, int idActividad)
        {
            return Ok(await _mediator.Send(new DeleteActividadCotizacionCommand
            {
                IdCotizacion = idCotizacion,
                IdActividad = idActividad
            }));
        }

        [HttpGet(template: "Tipos")]
        [SwaggerOperation("Retorna las tipos de cotizacion")]
        [SwaggerResponse(200, "Retorna los tipos", typeof(IEnumerable<ActividadCotizancionTreeNode>))]
        public async Task<IActionResult> GetTiposCotizacion()
        {
            return Ok(await _mediator.Send(new GetTipoCotizacionQuery()));
        }



        [HttpGet(template: "{idCotizacion}/DetalleOrden")]
        [SwaggerOperation("Retorna los productos de la cotizacion")]
        [SwaggerResponse(200, "Retorna los tipos", typeof(IEnumerable<DetalleOrdenModel>))]
        public async Task<IActionResult> GetDetalleOrdenByIdCotizacion(int idCotizacion)
        {
            return Ok(await _mediator.Send(new GetDetalleOrdenByCotizacionQuery { IdCotizacion = idCotizacion }));
        }

        [HttpPost(template: "{idCotizacion}/DetalleOrden")]
        [SwaggerOperation("Asigna un producto con su cantidad y precio a una cotizacion")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RegisterDetalleOrdenCotizacion(int idCotizacion, [FromBody] RegisterDetalleOrdenCotizacionCommand request)
        {
            request.IdCotizacion = idCotizacion;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(template: "{idCotizacion}/DetalleOrden/{idDetalleOrden}")]
        [SwaggerOperation("Actualiza el detalle orden de una cotizacion")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateDetalleOrdenCotizacion(int idCotizacion, int idDetalleOrden, [FromBody] UpdateDetalleOrdenCotizacionCommand request)
        {
            request.IdCotizacion = idCotizacion;
            request.IdDetalleOrden = idDetalleOrden;
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete(template: "{idCotizacion}/DetalleOrden/{idDetalleOrden}")]
        [SwaggerOperation("Elimina un detalle orden de la cotizacion en base a su idDetalleOrden")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> DeleteDetalleOrdenCotizacion(int idCotizacion, int idDetalleOrden)
        {
            return Ok(await _mediator.Send(new DeleteDetalleOrdenCotizacionCommand
            {
                IdCotizacion = idCotizacion,
                IdDetalleOrden = idDetalleOrden
            }));
        }
    }
}