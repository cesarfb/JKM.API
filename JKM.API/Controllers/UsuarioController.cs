using JKM.APPLICATION.Aggregates;
using JKM.APPLICATION.Commands.Usuario.DeleteUsuario;
using JKM.APPLICATION.Commands.Usuario.RegisterUsuario;
using JKM.APPLICATION.Commands.Usuario.UpdateEstado;
using JKM.APPLICATION.Commands.Usuario.UpdateUsuario;
using JKM.APPLICATION.Queries.Usuario.GetUsuarioById;
using JKM.APPLICATION.Queries.Usuario.GetUsuarioPaginado;
using JKM.UTILITY.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JKM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation("Retorna los usuarios por pagina")]
        [SwaggerResponse(200, "Retorna los usuarios", typeof(IEnumerable<UsuarioModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetUsuarioPaginado()
        {
            return Ok(await _mediator.Send(new GetUsuarioPaginadoQuery()));
        }

        [HttpGet(template: "{idUsuario}")]
        [SwaggerOperation("Retorna un usuario en base a su Id")]
        [SwaggerResponse(200, "Retorna la venta", typeof(UsuarioModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetUsuarioById(int idUsuario)
        {
            return Ok(await _mediator.Send(new GetUsuarioByIdQuery { IdUsuario = idUsuario }));
        }

        [HttpPost]
        [SwaggerOperation("Registrar un nuevo Usuario")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrió un error de validación", typeof(ErrorModel))]

        public async Task<IActionResult> RegisterUsuario([FromBody] RegisterUsuarioCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(template: "{idUsuario}")]
        [SwaggerOperation("Actualizar un Usuario en base a su ID")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateUsuario(int idUsuario, [FromBody] UpdateUsuarioCommand request)
        {
            request.IdUsuario = idUsuario;
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(template: "{idUsuario}/Estado")]
        [SwaggerOperation("Actualizar estado de un Usuario en base a su ID")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateEstado(int idUsuario)
        {
            return Ok(await _mediator.Send(new UpdateEstadoCommand { IdUsuario = idUsuario }));
        }

        [HttpDelete(template: "{idUsuario}")]
        [SwaggerOperation("Elimina un usuario en base a su IdUsuario")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> DeleteUsuario(int idUsuario)
        {
            return Ok(await _mediator.Send(new DeleteUsuarioCommand { IdUsuario = idUsuario }));
        }

    }
}
