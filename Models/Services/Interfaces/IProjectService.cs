using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectFilter filter, CancellationToken cancellationToken = default);
        Task<ProjectResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken token = default);
        Task<ProjectResponse> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken token = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task EnsureProjectExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
