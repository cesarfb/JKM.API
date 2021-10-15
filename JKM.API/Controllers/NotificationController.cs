using System.IO;
using System.Threading.Tasks;
using JKM.APPLICATION.Commands.Notification.ContactUs;
using JKM.UTILITY.Utils;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace JKM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
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

        [HttpPost(template: "Contact")]
        [SwaggerOperation("Envia un correo de notificacion de una cotizacion")]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> ContactUs([FromBody] ContactUsNotificationCommand request)
        {
            request.Path = Path.GetFullPath(Path.Combine(_env.ContentRootPath, "Reports/Templates/ContactUsHtml.html"));
            request.Logo = Path.GetFullPath(Path.Combine(_env.ContentRootPath, "Reports/Assets/JKMLOGO.png"));
            await _mediator.Publish(request);
            return Ok();
        }
    }
}
