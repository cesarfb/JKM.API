using MediatR;
using Microsoft.AspNetCore.Mvc;
using JKM.APPLICATION.Queries.Cliente.GetClientePaginado;
using JKM.APPLICATION.Commands.Cliente.RegisterCliente;
using JKM.APPLICATION.Commands.Cliente.UpdateCliente;
using JKM.APPLICATION.Aggregates;
using System.Threading.Tasks;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using JKM.UTILITY.Utils;
using Microsoft.AspNetCore.Authorization;

namespace JKM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation("Retorna los clientes por página")]
        [SwaggerResponse(200, "Retorna los clientes", typeof(PaginadoResponse<ClienteModel>))]
        [SwaggerResponse(204, "No se encontraron clientes")]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetClientePaginado()
        {
            return Ok(await _mediator.Send(new GetClientePaginadoQuery()));
        }

        [HttpPost]
        [SwaggerOperation("Registra un nuevo cliente")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> RegisterCliente([FromBody] RegisterClienteCommand request)
        {
            return Ok(await _mediator.Send(request));
        }


        [HttpPut(template: "{idCliente}")]
        [SwaggerOperation("Actualiza un cliente en base a su Id")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateCliente(int idCliente, [FromBody] UpdateClienteCommand request)
        {
            request.IdCliente = idCliente;
            return Ok(await _mediator.Send(request));
        }
    }
}
