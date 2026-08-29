using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TaskManagerWebApi.Models.Entities
{
    public class WorkItemEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; } = null!;
    }
}
