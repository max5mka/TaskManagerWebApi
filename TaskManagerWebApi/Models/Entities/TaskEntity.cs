using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Enums;

namespace TaskManagerWebApi.Models.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; } // название
        public required string Description { get; set; } // описание
        public string Status { get; set; } = Statuses.New; // статус
        public required string Priority { get; set; } // приоритет
        public required double HoursToDo { get; set; } // время на решение задачи
        public double HoursLogged { get; set; } = 0.0; // залогированное время
        public DateTime? TakenAt { get; set; }
        public required DateTime Deadline { get; set; } // срок выполнения задачи
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public required int CreatorId { get; set; }
        public UserEntity Creator { get; set; } = null!;

        public required int ProjectId { get; set; }
        public ProjectEntity Project { get; set; } = null!;


        public int? AssigneeId { get; set; }
        public UserEntity? Assignee { get; set; } = null!;
    }
}
