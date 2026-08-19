namespace TaskManagerWebApi.Models.Repositories
{
    public interface IWorkItemRepository
    {
        IQueryable<WorkItem> GetAll();
        Task<WorkItem?> GetByIdAsync(int id);
    }
}
