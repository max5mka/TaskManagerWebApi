using Microsoft.EntityFrameworkCore;

namespace TaskManagerWebApi.Models.Repositories
{
    public class WorkItemRepository(ApplicationDbContext context) : IWorkItemRepository
    {
        public IQueryable<WorkItem> GetAll()
        {
            return context.WorkItems;
        }

        public async Task<WorkItem?> GetByIdAsync(int id)
        {
            return await context.WorkItems.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
