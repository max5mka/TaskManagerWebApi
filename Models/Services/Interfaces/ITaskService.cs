using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponse>> GetAllAsync(
            int projectId, TaskFilter filter, CancellationToken cancellationToken = default);
        Task<TaskResponse> GetByIdAsync(
            int projectId, int taskId, CancellationToken cancellationToken = default);
        Task<TaskResponse> CreateAsync(
            int projectId, CreateTaskRequest request, CancellationToken cancellationToken = default);
        Task<TaskResponse> UpdateAsync(
            int projectId, int taskId, UpdateTaskRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(
            int projectId, int taskId, CancellationToken cancellationToken = default);
    }
}
