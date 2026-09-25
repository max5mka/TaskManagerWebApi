using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskShortResponse>> GetAllAsync(int projectId, TaskFilter filter, CancellationToken cancellationToken = default);
        Task<TaskLongResponse> GetByIdAsync(int projectId, int taskId, CancellationToken cancellationToken = default);
        Task<TaskCreateResponse> CreateAsync(int projectId, TaskCreateRequest request, CancellationToken cancellationToken = default);
        Task<TaskLongResponse> UpdateAsync(int projectId, int taskId, TaskUpdateRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(int projectId, int taskId, CancellationToken cancellationToken = default);
    }
}
