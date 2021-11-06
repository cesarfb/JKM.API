using System.Threading.Tasks;
using JKM.APPLICATION.Commands.Notification.ContactUs;
using JKM.UTILITY.Utils;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using JKM.APPLICATION.Commands.Notification.Cotizacion;

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
            request.Path = "Reports/Templates/ContactUsHtml.html";
            await _mediator.Publish(request);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost(template: "Cotizacion")]
        [SwaggerOperation("Envia un correo de notificacion de una cotizacion")]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> Cotizacion([FromBody] CotizacionNotificationCommand request)
        {
            request.Path = "Reports/Templates/CotizacionHtml.html";
            await _mediator.Publish(request);
            return Ok();
        }
    }
}
