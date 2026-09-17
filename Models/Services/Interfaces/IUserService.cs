using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllAsync(UserFilter filter, CancellationToken cancellationToken = default);
        Task<UserResponse> GetByIdAsync(int userId, CancellationToken cancellationToken = default);
        Task EnsureUserExistsAsync(int userId, CancellationToken cancellationToken = default);
    }
}
