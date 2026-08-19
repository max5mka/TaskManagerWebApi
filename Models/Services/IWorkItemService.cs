namespace TaskManagerWebApi.Models.Services
{
    public interface IWorkItemService
    {
        Task<IEnumerable<WorkItem>> GetAllAsync(string? status, string? priority, int page, int pageSize);
        Task<WorkItem?> GetByIdAsync(int id);
    }
}
