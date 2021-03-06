using System.Threading.Tasks;
using JKM.APPLICATION.Commands.Notification.ContactUs;
using JKM.UTILITY.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using JKM.APPLICATION.Aggregates;
using System.Collections.Generic;
using JKM.APPLICATION.Queries.Cotizacion.GetCotizacionById;
using JKM.APPLICATION.Queries.Cotizacion.GetTrabajadoresByCotizacion;
using JKM.APPLICATION.Queries.Cotizacion.GetActividadesByCotizacion;
using JKM.APPLICATION.Queries.Cotizacion.GetDetalleOrdenByCotizacion;
using JKM.APPLICATION.Commands.Notification.AceptarCotizacion;
using JKM.APPLICATION.Commands.Notification.RecuperarUsuario;
using JKM.APPLICATION.Commands.Notification.EnviarCotizacion;

namespace JKM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost(template: "Contact")]
        [SwaggerOperation("Envia un correo de notificacion de una solicitud")]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> ContactUs([FromBody] ContactUsNotificationCommand request)
        {
            await _mediator.Publish(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost(template: "Cotizacion")]
        [SwaggerOperation("Envia un correo de notificacion de una cotizacion")]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> Cotizacion(EnviarCotizacionCommand request)
        {
            await _mediator.Publish(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost(template: "Cotizacion/{idCotizacion}")]
        [SwaggerOperation("Envia un correo de notificacion de una cotizacion")]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> Cotizacion(int idCotizacion)
        {
            CotizacionModel cotizacion = await _mediator.Send(new GetCotizacionByIdQuery { IdCotizacion = idCotizacion });
            IEnumerable<ActividadCotizancionTreeNode> actividades = await _mediator.Send(new GetActividadesByCotizacionQuery { IdCotizacion = idCotizacion });
            IEnumerable<TipoTrabajadorModel> trabajadores = await _mediator.Send(new GetTrabajadoresByCotizacionQuery { IdCotizacion = idCotizacion });
            IEnumerable<DetalleOrdenModel> productos = await _mediator.Send(new GetDetalleOrdenByCotizacionQuery { IdCotizacion = idCotizacion });

            await _mediator.Publish(new AceptarCotizacionNotificationCommand()
            {
                Cotizacion = cotizacion,
                Trabajadores = trabajadores,
                Actividades = actividades,
                Productos = productos
            });
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost(template: "Auth/RecuperarUsuario")]
        [SwaggerOperation("Envia un correo con las credenciales")]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RecuperarUsuario(RecuperarUsuarioNotificationCommand request)
        {
            await _mediator.Publish(request);
            return Ok();
        }
    }
}
