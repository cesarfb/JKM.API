using JKM.APPLICATION.Queries.Pedido.GetPedido;
using JKM.APPLICATION.Queries.Pedido.GetPedidoById;
using JKM.UTILITY.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace JKM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PedidoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation("Retorna los pedidos por página")]
        [SwaggerResponse(200, "Retorna los pedidos", typeof(PaginadoResponse<GetPedidoQuery>))]
        [SwaggerResponse(204, "No se encontraron pedidos")]
        public async Task<IActionResult> GetPedidosPaginado()
        {
            return Ok(await _mediator.Send(new GetPedidoQuery()));
        }


        [HttpGet(template: "{idPedido}")]
        [SwaggerOperation("Retorna el pedido en base a su id")]
        [SwaggerResponse(200, "Retorna el pedido", typeof(PaginadoResponse<GetPedidoQuery>))]
        [SwaggerResponse(204, "No se encontro el pedido")]
        public async Task<IActionResult> GetPedidoById(int idPedido)
        {
            return Ok(await _mediator.Send(new GetPedidoByIdQuery() { IdPedido = idPedido }));
        }
    }
}
