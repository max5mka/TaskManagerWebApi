using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<UserResponse>> GetAllUsers(UserFilter filter, CancellationToken cancellationToken = default);
    }
}
