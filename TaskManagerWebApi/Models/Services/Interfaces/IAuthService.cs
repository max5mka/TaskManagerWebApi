using TaskManagerWebApi.Models.Requests;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(UserRegisterRequest request, CancellationToken cancellationToken = default);
        Task<(string AccessToken, string RefreshToken)> LoginAsync(UserAuthorizeRequest request, CancellationToken cancellationToken = default);
        Task<(string AccessToken, string RefreshToken)> RefreshAsync(string refreshToken, CancellationToken cancellationToken = default);
        Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    }
}
