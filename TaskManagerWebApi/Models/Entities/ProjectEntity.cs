namespace TaskManagerWebApi.Models.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; } // название
        public required string Description { get; set; } // описание
        public required string Status { get; set; } // статус
        public required DateTime Start { get; set; } // начало сроков
        public required DateTime Deadline { get; set; } // конец сроков
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public required int CreatorId { get; set; }
        public UserEntity Creator { get; set; } = null!;

        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>(); // задачи
        public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>(); // участники

        public double TotalHours => (Deadline - Start).TotalHours; // всего часов
        public double HoursSpent => Tasks.Sum(x => x.HoursToDo); // потрачено на задачи
    }
}
