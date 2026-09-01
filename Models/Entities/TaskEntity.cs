using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TaskManagerWebApi.Models.Enums;

namespace TaskManagerWebApi.Models.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; } // название
        public required string Description { get; set; } // описание
        public required string Status { get; set; } // статус
        public required PriorityEnum Priority { get; set; } // приоритет
        public required double Hours { get; set; } // часы
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public int ProjectId { get; set; }
        public ProjectEntity Project { get; set; } = null!;
    }
}
