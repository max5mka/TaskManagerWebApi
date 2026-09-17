using TaskManagerWebApi.Models.Requests;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(UserRegisterRequest request, CancellationToken cancellationToken = default);
        Task<string> LoginAsync(UserAuthorizeRequest request, CancellationToken cancellationToken = default);
    }
}
