using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Data;
using TaskManagerWebApi.Exceptions;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Models.Services
{
    public class UserService(ApplicationDbContext _context) : IUserService
    {
        public async Task EnsureUserExistsByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            if (!await _context.Users.AnyAsync(x => x.Id == userId, cancellationToken))
                throw new NotFoundException($"User with id={userId} not found");
        }

        public async Task EnsureUserExistsByLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            if (!await _context.Users.AnyAsync(x => x.Login == login, cancellationToken))
                throw new NotFoundException($"User with login {login} not found");
        }
    }
}
