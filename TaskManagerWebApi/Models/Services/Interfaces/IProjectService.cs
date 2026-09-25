using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectShortResponse>> GetAllAsync(ProjectFilter filter, CancellationToken cancellationToken = default);
        Task<ProjectLongResponse> GetByIdAsync(int projectId, CancellationToken cancellationToken = default);
        Task<ProjectCreateResponse> CreateAsync(ProjectCreateRequest request, CancellationToken token = default);
        Task<ProjectLongResponse> UpdateAsync(int projectId, ProjectUpdateRequest request, CancellationToken token = default);
        Task DeleteAsync(int projectId, CancellationToken cancellationToken = default);
        Task EnsureProjectExistsAsync(int projectId, CancellationToken cancellationToken = default);
    }
}
