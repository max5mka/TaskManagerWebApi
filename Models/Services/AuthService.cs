using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Authentication;
using TaskManagerWebApi.Data;
using TaskManagerWebApi.Exceptions;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Models.Services
{
    public class AuthService(
        ApplicationDbContext _context,
        IJWTService _jwtService) : IAuthService
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

        public async Task<string> LoginAsync(UserAuthorizeRequest request, CancellationToken cancellationToken = default)
        {
            var found = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken);

            if (found == null)
                throw new UnauthorizedException("Invalid login or password");

            var result = new PasswordHasher<UserEntity>()
                .VerifyHashedPassword(found, found.HashedPassword, request.Password);

            if (result == PasswordVerificationResult.Success)
            {
                return _jwtService.GenerateToken(found);
            }
            else
            {
                throw new UnauthorizedException("Invalid login or password");
            }
        }
    }
}
