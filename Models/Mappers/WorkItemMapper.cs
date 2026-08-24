using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Mappers
{
    public static class WorkItemMapper
    {
        public static WorkItemResponse ToResponse(this WorkItem entity)
        {
            return new WorkItemResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                Priority = entity.Priority,
            };
        }
    }
}
