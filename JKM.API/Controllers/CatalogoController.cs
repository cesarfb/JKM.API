using JKM.APPLICATION.Aggregates;
using JKM.APPLICATION.Commands.Catalogo.RegisterCatalogo;
using JKM.APPLICATION.Commands.Catalogo.UpdateCatalogo;
using JKM.APPLICATION.Commands.Catalogo.UpdateEstadoCatalogo;
using JKM.APPLICATION.Queries.Catalogo.GetCatalogoById;
using JKM.APPLICATION.Queries.Catalogo.GetCatalogoPaginado;
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

    public class CatalogoController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CatalogoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation("Retorna los Catálogos por pagina")]
        [SwaggerResponse(200, "Retorna los catálogos", typeof(IEnumerable<CatalogoModel>))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetCatalogoPaginado()
        {
            return Ok(await _mediator.Send(new GetCatalogoPaginadoQuery()));
        }

        [HttpGet(template: "{idCatalogo}")]
        [SwaggerOperation("Retorna un catálogo en base a su Id")]
        [SwaggerResponse(200, "Retorna el catálogo", typeof(CatalogoModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> GetCatalogoById(int idCatalogo)
        {
            return Ok(await _mediator.Send(new GetCatalogoByIdQuery { IdCatalogo = idCatalogo }));
        }

        [HttpPost]
        [SwaggerOperation("Registrar un nuevo Catálogo")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrió un error de validación", typeof(ErrorModel))]

        public async Task<IActionResult> RegisterCatalogo([FromBody] RegisterCatalogoCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut(template: "{idCatalogo}")]
        [SwaggerOperation("Actualizar un Catalogo en base a su ID")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateCatalogo(int idCatalogo, [FromBody] UpdateCatalogoCommand request)
        {
            request.IdCatalogo = idCatalogo;

            return Ok(await _mediator.Send(request));
        }


        [HttpPut(template: "{idCatalogo}/Estado")]
        [SwaggerOperation("Actualizar estado de un Catalogo en base a su ID")]
        [SwaggerResponse(200, "Retorna mensaje de exito", typeof(ResponseModel))]
        [SwaggerResponse(400, "Ocurrio un error de validacion", typeof(ErrorModel))]
        public async Task<IActionResult> UpdateEstado(int idCatalogo)
        {
            return Ok(await _mediator.Send(new UpdateEstadoCatalogoCommand { IdCatalogo = idCatalogo }));
        }


    }
}
