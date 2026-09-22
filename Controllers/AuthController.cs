using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthController(IAuthService _service) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            await _service.RegisterAsync(request);
            return NoContent();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserAuthorizeRequest request)
        {
            var token = await _service.LoginAsync(request);
            return Ok(token);
        }
    }
}
