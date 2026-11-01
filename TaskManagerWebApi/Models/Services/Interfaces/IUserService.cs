namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IUserService
    {
        Task EnsureUserExistsByIdAsync(int userId, CancellationToken cancellationToken = default);
        Task EnsureUserExistsByLoginAsync(string login, CancellationToken cancellationToken = default);
    }
}
