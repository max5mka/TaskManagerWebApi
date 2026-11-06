using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Options;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AuthController(
        IAuthService _service,
        IOptions<AuthSettings> _authOptions) : ControllerBase
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
            var tokens = await _service.LoginAsync(request);
            SetRefreshTokenCookie(tokens.RefreshToken);
            return Ok(tokens.AccessToken);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
        {
            if (!Request.Cookies.TryGetValue("refresh_token", out var refreshToken)
                || string.IsNullOrWhiteSpace(refreshToken))
            {
                return Unauthorized();
            }

            var tokens = await _service.RefreshAsync(refreshToken, cancellationToken);
            SetRefreshTokenCookie(tokens.RefreshToken);
            return Ok(tokens.AccessToken);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            if (Request.Cookies.TryGetValue("refresh_token", out var refreshToken)
                && !string.IsNullOrWhiteSpace(refreshToken))
            {
                await _service.RevokeRefreshTokenAsync(refreshToken, cancellationToken);
            }

            Response.Cookies.Delete("refresh_token");
            return NoContent();
        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.Add(_authOptions.Value.RefreshTokenExpires),
                Path = "/api"
            });
        }
    }
}
