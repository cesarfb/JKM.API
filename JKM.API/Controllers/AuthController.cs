using System.Threading.Tasks;
using JKM.UTILITY.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JKM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate()
        {
            string token = JwtHandler.createToken();
            var decode = JwtHandler.decodeToken(token);
            var validate = JwtHandler.validate(token);
            return Ok(token);
        }
    }
}
