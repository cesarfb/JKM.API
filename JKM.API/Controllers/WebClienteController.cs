using System.Collections.Generic;
using System.Threading.Tasks;
using JKM.APPLICATION.Aggregates;
using JKM.APPLICATION.Queries.WebCliente.GetProductos;
using JKM.APPLICATION.Queries.WebCliente.GetServicios;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebClienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WebClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(template: "Servicios")]
        [SwaggerOperation("Retorna las servicios de la empresa")]
        [SwaggerResponse(200, "Retorna los servicios", typeof(IEnumerable<ServicioWebModel>))]
        public async Task<IActionResult> GetServicios()
        {
            return Ok(await _mediator.Send(new GetServiciosQuery()));
        }

        [HttpGet(template: "Catalogo")]
        [SwaggerOperation("Retorna las productos de la empresa")]
        [SwaggerResponse(200, "Retorna los productos", typeof(IEnumerable<CatalogoWebModel>))]
        public async Task<IActionResult> GetProductos()
        {
            return Ok(await _mediator.Send(new GetCatalogoQuery()));
        }
    }
}
