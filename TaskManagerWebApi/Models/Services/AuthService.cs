using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using TaskManagerWebApi.Data;
using TaskManagerWebApi.Exceptions;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Models.Services
{
    public class AuthService(
        ApplicationDbContext _context,
        IJWTService _jwtService,
        IOptions<AuthSettings> _authOptions) : IAuthService
    { 
        public async Task RegisterAsync(UserRegisterRequest request, CancellationToken cancellationToken = default)
        {
            if (await _context.Users.AnyAsync(x => x.Login == request.Login))
                throw new AlreadyExistsException($"User with login {request.Login} alredy exists");

            var newUser = new UserEntity
            { 
                FirstName = request.FirstName,
                Login = request.Login,
            };

            var hashedPassword = new PasswordHasher<UserEntity>().HashPassword(newUser, request.Password);
            newUser.HashedPassword = hashedPassword;

            await _context.Users.AddAsync(newUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<(string AccessToken, string RefreshToken)> LoginAsync(UserAuthorizeRequest request, CancellationToken cancellationToken = default)
        {
            var found = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken);

            if (found == null)
                throw new UnauthorizedException("Invalid login or password");

            var result = new PasswordHasher<UserEntity>()
                .VerifyHashedPassword(found, found.HashedPassword, request.Password);

            if (result == PasswordVerificationResult.Success)
            {
                var refreshToken = await CreateRefreshTokenAsync(found.Id, cancellationToken);
                return (_jwtService.GenerateToken(found), refreshToken);
            }
            else
            {
                throw new UnauthorizedException("Invalid login or password");
            }
        }

        public async Task<(string AccessToken, string RefreshToken)> RefreshAsync(
            string refreshToken,
            CancellationToken cancellationToken = default)
        {
            var tokenHash = HashRefreshToken(refreshToken);
            var storedToken = await _context.RefreshTokens
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);

            if (storedToken == null
                || storedToken.RevokedAt.HasValue
                || storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                throw new UnauthorizedException("Invalid or expired refresh token");
            }

            storedToken.RevokedAt = DateTime.UtcNow;
            var newRefreshToken = await CreateRefreshTokenAsync(storedToken.UserId, cancellationToken);

            return (_jwtService.GenerateToken(storedToken.User), newRefreshToken);
        }

        public async Task RevokeRefreshTokenAsync(
            string refreshToken,
            CancellationToken cancellationToken = default)
        {
            var tokenHash = HashRefreshToken(refreshToken);
            var storedToken = await _context.RefreshTokens
                .SingleOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);

            if (storedToken != null && !storedToken.RevokedAt.HasValue)
            {
                storedToken.RevokedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        private async Task<string> CreateRefreshTokenAsync(
            int userId,
            CancellationToken cancellationToken)
        {
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            _context.RefreshTokens.Add(new RefreshTokenEntity
            {
                TokenHash = HashRefreshToken(refreshToken),
                ExpiresAt = DateTime.UtcNow.Add(_authOptions.Value.RefreshTokenExpires),
                UserId = userId
            });

            await _context.SaveChangesAsync(cancellationToken);
            return refreshToken;
        }

        private static string HashRefreshToken(string refreshToken) =>
            Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));
    }
}
