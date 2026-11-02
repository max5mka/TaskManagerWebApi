namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IProjectAccessService
    {
        Task EnsureUserIsMemberAsync(int projectId, CancellationToken cancellationToken = default);
        Task EnsureUserIsOwnerAsync(int projectId, CancellationToken cancellationToken = default);
    }
}
