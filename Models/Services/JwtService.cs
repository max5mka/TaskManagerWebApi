using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Models.Services
{
    public class JWTService(IOptions<AuthSettings> options) : IJWTService
    {
        public string GenerateToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim("login", user.Login),
                new Claim("firstname", user.FirstName),
                new Claim("id", user.Id.ToString()),
            };

            var jwtToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.Add(options.Value.Expires),
                claims: claims,
                signingCredentials:
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            options.Value.SecretKey)),
                        SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
