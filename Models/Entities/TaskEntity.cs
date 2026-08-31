using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TaskManagerWebApi.Models.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public required int NumberOfHours { get; set; }
        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; } = null!;
    }
}
