namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IUserService
    {
        Task EnsureUserExistsAsync(int userId, CancellationToken cancellationToken = default);
    }
}
