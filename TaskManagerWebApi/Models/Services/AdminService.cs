using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Data;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Mappers;
using TaskManagerWebApi.Models.Response;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Models.Services
{
    public class AdminService(ApplicationDbContext _context) : IAdminService
    {
        public async Task<IEnumerable<UserResponse>> GetAllUsers(UserFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Users
                .Where(x => !x.IsAdmin)
                .OrderBy(x => x.Id)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var users = await query.ToListAsync(cancellationToken);
            return UserMapper.ToResponses(users);
        }
    }
}
