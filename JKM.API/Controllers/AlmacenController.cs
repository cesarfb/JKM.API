using JKM.APPLICATION.Commands.Almacen.RegisterAlmacen;
using JKM.APPLICATION.Commands.Almacen.UpdateAlmacen;
using JKM.APPLICATION.Queries.Almacen.GetAlmacen;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JKM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlmacenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation("Retorna los almacenes por página")]
        public async Task<IActionResult> GetAlmacenPaginado()
        {
            return Ok(await _mediator.Send(new GetAlmacenQuery()));
        }

        [HttpPost]
        [SwaggerOperation("Registra un nuevo almacen")]
        public async Task<IActionResult> RegisterAlmacen(RegisterAlmacenCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(template: "{idAlmacen}")]
        [SwaggerOperation("Actualiza un almacen en base a su ID")]
        public async Task<IActionResult> UpdateAlmacen(int idAlmacen, [FromBody] UpdateAlmacenCommand request)
        {
            request.IdAlmacen = idAlmacen;
            return Ok(await _mediator.Send(request));
        }
    }
}
