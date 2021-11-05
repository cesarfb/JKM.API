using MediatR;
using Microsoft.AspNetCore.Mvc;
using JKM.APPLICATION.Queries.Cliente.GetClientePaginado;
using JKM.APPLICATION.Queries.Producto.GetProducto;
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
    public class ProductoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation("Retorna los productos")]
        [SwaggerResponse(200, "Retorna los productos", typeof(IEnumerable<ProductoModel>))]
        [SwaggerResponse(204, "No se encontraron productos")]
        public async Task<IActionResult> GetProducto([FromQuery] GetProductoQuery request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
