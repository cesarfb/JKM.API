using System.Threading.Tasks;
using JKM.APPLICATION.Commands.Auth.LoginUser;
using JKM.APPLICATION.Commands.Auth.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JKM.APPLICATION.Queries.Auth.GetPerfilUsuario;
using JKM.UTILITY.Jwt;
using System.Collections.Generic;
using System.Security.Claims;
using JKM.APPLICATION.Aggregates;
using Swashbuckle.AspNetCore.Annotations;
using JKM.UTILITY.Utils;

namespace JKM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtService _jwtService;

        public AuthController(IMediator mediator, IJwtService jwtService)
        {
            _mediator = mediator;
            _jwtService = jwtService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(template: "Login")]
        [SwaggerResponse(200, "Retorna los datos basicos del usuario", typeof(ResponseModel))]
        [SwaggerResponse(502, "Credenciales incorrectas", typeof(ErrorModel))]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
        
        [HttpPost]
        [Route(template: "Register")]
        [SwaggerResponse(400, "Ocurrio un error de validacion")]
        [SwaggerResponse(401, "Inautorizado", type: null)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        [Route(template: "Perfil")]
        [SwaggerResponse(200, "Retorna los datos basicos del usuario", typeof(PerfilModel))]
        [SwaggerResponse(401, "Inautorizado")]
        public async Task<IActionResult> GetPerfilUsuario()
        {
            string authHeader = Request.Headers["Authorization"];
            string token = authHeader.Replace("Bearer ", "");
            List<Claim> claims = _jwtService.DecodeToken(token);
            return Ok(await _mediator.Send(new GetPerfilUsuarioQuery {
                IdUsuario = int.Parse(claims[0].Value)
            }));
        }
    }
}
