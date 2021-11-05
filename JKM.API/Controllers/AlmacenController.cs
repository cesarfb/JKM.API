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
        //[SwaggerResponse(200, "Retorna los clientes", typeof(PaginadoResponse<ClienteModel>))]
        //[SwaggerResponse(204, "No se encontraron clientes")]
        //[SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetAlmacenPaginado()
        {
            return Ok(await _mediator.Send(new GetAlmacenQuery()));
        }
    }
}
