using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services
{
    public interface IWorkItemService
    {
        Task<IEnumerable<WorkItemResponse>> GetAllAsync(WorkItemFilter filter, CancellationToken cancellationToken = default);
        Task<WorkItemResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<WorkItemResponse> CreateAsync(CreateWorkItemRequest request, CancellationToken token = default);
        Task UpdateAsync(int id, UpdateWorkItemRequest request, CancellationToken token = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
