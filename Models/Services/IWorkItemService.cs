using TaskManagerWebApi.Models.DTOs;
using TaskManagerWebApi.Models.Entities;
using static System.Net.WebRequestMethods;

namespace TaskManagerWebApi.Models.Services
{
    public interface IWorkItemService
    {
        Task<IEnumerable<WorkItem>> GetAllAsync(WorkItemFilterDTO filter);
        Task<WorkItem?> GetByIdAsync(int id);
    }
}
