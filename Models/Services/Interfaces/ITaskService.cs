using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskShortResponse>> GetAllAsync(
            int userId, int projectId, TaskFilter filter, CancellationToken cancellationToken = default);
        Task<TaskLongResponse> GetByIdAsync(
            int userId, int projectId, int taskId, CancellationToken cancellationToken = default);
        Task<TaskCreateResponse> CreateAsync(
            int userId, int projectId, TaskCreateRequest request, CancellationToken cancellationToken = default);
        Task<TaskLongResponse> UpdateAsync(
            int userId, int projectId, int taskId, TaskUpdateRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(
            int userId, int projectId, int taskId, CancellationToken cancellationToken = default);
    }
}
