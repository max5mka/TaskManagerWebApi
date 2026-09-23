namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        void EnsureUserExists();
    }
}
