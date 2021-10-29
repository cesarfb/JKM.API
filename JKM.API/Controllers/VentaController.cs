using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JKM.APPLICATION.Queries.Venta.GetCuotasVentaById;
using JKM.APPLICATION.Queries.Venta.GetEstadoVenta;
using JKM.APPLICATION.Queries.Venta.GetTipoVenta;
using JKM.APPLICATION.Queries.Venta.GetVentaById;
using JKM.APPLICATION.Queries.Venta.GetVentaPaginado;
using Swashbuckle.AspNetCore.Annotations;
using JKM.UTILITY.Utils;
using System.Collections.Generic;
using JKM.APPLICATION.Aggregates;
using Microsoft.AspNetCore.Authorization;
using JKM.APPLICATION.Commands.Venta.RegisterVenta;
using JKM.APPLICATION.Commands.Venta.DeleteVenta;

namespace JKM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VentaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation("Retorna las ventas por pagina")]
        [SwaggerResponse(200, "Retorna las ventas", typeof(IEnumerable<VentaModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetVentaPaginado()
        {
            return Ok(await _mediator.Send(new GetVentaPaginadoQuery()));
        }

        [HttpGet(template: "{idVenta}")]
        [SwaggerOperation("Retorna una venta en base a su Id")]
        [SwaggerResponse(200, "Retorna la venta", typeof(VentaModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetVentaById(int idVenta)
        {
            return Ok(await _mediator.Send(new GetVentaByIdQuery { IdVenta = idVenta }));
        }

        [HttpPost]
        [SwaggerOperation("Registrar una nueva Cuota por Venta")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrió un error de validación", typeof(ErrorModel))]

        public async Task<IActionResult> RegisterCuotasxVenta([FromBody] RegisterVentaCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet(template: "{idVenta}/Cuotas")]
        [SwaggerOperation("Retorna cuotas de una venta en base a su id")]
        [SwaggerResponse(200, "Retorna las cuotas", typeof(VentaCuotasModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetCuotasVentaById(int idVenta)
        {
            return Ok(await _mediator.Send(new GetCuotasVentaByIdQuery { IdVenta = idVenta }));
        }

        [HttpDelete(template: "{idVenta}")]
        [SwaggerOperation("Elimina una venta en base a su IdVenta")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> DeleteVentas(int idVenta)
        {
            return Ok(await _mediator.Send(new DeleteVentaCommand { IdVenta = idVenta }));
        }




















        [HttpGet(template: "Estado")]
        [SwaggerOperation("Retorna los estados de ventas")]
        [SwaggerResponse(200, "Retorna los estados", typeof(IEnumerable<Identifier>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetEstadosVenta()
        {
            return Ok(await _mediator.Send(new GetEstadoVentaQuery()));
        }

        [HttpGet(template: "Tipo")]
        [SwaggerOperation("Retorna los tipos de ventas")]
        [SwaggerResponse(200, "Retorna los tipos", typeof(IEnumerable<Identifier>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetTiposVenta()
        {
            return Ok(await _mediator.Send(new GetTipoVentaQuery()));
        }

    }
}
