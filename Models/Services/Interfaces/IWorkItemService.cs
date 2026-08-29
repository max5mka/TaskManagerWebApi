using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IWorkItemService
    {
        Task<IEnumerable<WorkItemResponse>> GetAllAsync(
            int projectId, WorkItemFilter filter, CancellationToken cancellationToken = default);
        Task<WorkItemResponse> GetByIdAsync(
            int projectId, int workItemId, CancellationToken cancellationToken = default);
        Task<WorkItemResponse> CreateAsync(
            int projectId, CreateWorkItemRequest request, CancellationToken cancellationToken = default);
        Task<WorkItemResponse> UpdateAsync(
            int projectId, int workItemId, UpdateWorkItemRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(
            int projectId, int workItemId, CancellationToken cancellationToken = default);
    }
}
