using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Models.Repositories;

namespace TaskManagerWebApi.Models.Services
{
    public class WorkItemService(IWorkItemRepository _workItemRepository) : IWorkItemService
    {
        public async Task<IEnumerable<WorkItem>> GetAllAsync(string? status, string? priority, int page, int pageSize)
        {            
            var query = _workItemRepository.GetAll()
                .Where(x => status == null || x.Status == status)
                .Where(x => priority == null || x.Priority == priority)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<WorkItem?> GetByIdAsync(int id)
        {
            var workItem = await _workItemRepository.GetByIdAsync(id);
            if (workItem == null)
            {
                throw new Exception($"Task not found with Id={id}");
            }

            return workItem;
        }
    }
}
    