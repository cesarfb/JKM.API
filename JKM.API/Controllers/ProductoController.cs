using MediatR;
using Microsoft.AspNetCore.Mvc;
using JKM.APPLICATION.Queries.Producto.GetProducto;
using JKM.APPLICATION.Aggregates;
using System.Threading.Tasks;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using JKM.APPLICATION.Queries.Producto.GetProductoById;
using JKM.APPLICATION.Commands.Producto.RegisterProducto;
using JKM.APPLICATION.Commands.Producto.UpdateProducto;

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

        [HttpGet(template: "{idProducto}")]
        [SwaggerOperation("Retorna un producto en base a su id")]
        [SwaggerResponse(200, "Retorna el producto", typeof(IEnumerable<ProductoModel>))]
        [SwaggerResponse(204, "No se encontraro el producto")]
        public async Task<IActionResult> GetProductoById(int idProducto)
        {
            GetProductoByIdQuery request = new GetProductoByIdQuery();
            request.IdProducto = idProducto;
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        [SwaggerOperation("Agrega un producto")]
        public async Task<IActionResult> RegisterProducto([FromBody] RegisterProductoCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{idProducto}")]
        [SwaggerOperation("Actualiza un producto")]
        public async Task<IActionResult> UpdateProducto(int idProducto, [FromBody] UpdateProductoCommand request)
        {
            request.IdProducto = idProducto;
            return Ok(await _mediator.Send(request));
        }
    }
}
