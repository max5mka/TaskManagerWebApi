using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Models.DTOs;
using TaskManagerWebApi.Models.Entities;

namespace TaskManagerWebApi.Models.Services
{
    public class WorkItemService(ApplicationDbContext _context) : IWorkItemService
    {
        public async Task<IEnumerable<WorkItem>> GetAllAsync(WorkItemFilterDTO filter)
        {            
            var query = _context.WorkItems
                .Where(x => filter.Status == null || x.Status == filter.Status)
                .Where(x => filter.Priority == null || x.Priority == filter.Priority)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            return await query.ToListAsync();
        }

        public async Task<WorkItem?> GetByIdAsync(int id)
        {
            return await _context.WorkItems.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
    