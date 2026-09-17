using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectShortResponse>> GetAllAsync(int userId, ProjectFilter filter, CancellationToken cancellationToken = default);
        Task<ProjectLongResponse> GetByIdAsync(int userId, int projectId, CancellationToken cancellationToken = default);
        Task<ProjectCreateResponse> CreateAsync(int userId, ProjectCreateRequest request, CancellationToken token = default);
        Task<ProjectLongResponse> UpdateAsync(int userId, int projectId, ProjectUpdateRequest request, CancellationToken token = default);
        Task DeleteAsync(int userId, int projectId, CancellationToken cancellationToken = default);
        Task EnsureProjectExistsAsync(int userId, int projectId, CancellationToken cancellationToken = default);
    }
}
