using System.IO;
using System.Threading.Tasks;
using JKM.APPLICATION.Commands.Notification.ContactUs;
using JKM.PERSISTENCE.Utils;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public NotificationController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpPost(template: "ContactUs")]
        [SwaggerOperation("Envia un correo de notificacion de una cotizacion")]
        [SwaggerResponse(422, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetCotizacionPaginado([FromBody] ContactUsNotificationCommand request)
        {
            request.Path = Path.GetFullPath(Path.Combine(_env.ContentRootPath, "Reports/Templates/ContactUsHtml.html"));
            request.Logo = Path.GetFullPath(Path.Combine(_env.ContentRootPath, "Reports/Assets/JKMLOGO.png"));
            await _mediator.Publish(request);
            return Ok();
        }
    }
}
