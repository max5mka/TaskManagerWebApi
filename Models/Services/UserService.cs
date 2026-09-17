using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Data;
using TaskManagerWebApi.Exceptions;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Mappers;
using TaskManagerWebApi.Models.Response;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Models.Services
{
    public class UserService(ApplicationDbContext _context) : IUserService
    {
        public async Task<List<UserResponse>> GetAllAsync(UserFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Users
               .Skip((filter.Page - 1) * filter.PageSize)
               .Take(filter.PageSize);

            var entities = await query.ToListAsync(cancellationToken);

            return UserMapper.ToResponses(entities);
        }

        public async Task<UserResponse> GetByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            await EnsureUserExistsAsync(userId, cancellationToken);
            var found = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return UserMapper.ToResponse(found);
        }

        public async Task EnsureUserExistsAsync(int userId, CancellationToken cancellationToken = default)
        {
            if (!await _context.Users.AnyAsync(x => x.Id == userId, cancellationToken))
            {
                throw new NotFoundException($"User with id={userId} not found");
            }
        }
    }
}
