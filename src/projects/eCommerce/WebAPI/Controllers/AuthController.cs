using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Core.Security.JWT;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            AccessToken result = await Mediator.Send(loginCommand);

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            AccessToken result = await Mediator.Send(registerCommand);

            return Ok(result);
        }
    }
}