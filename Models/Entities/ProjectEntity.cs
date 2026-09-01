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
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<TaskEntity> Tasks = new List<TaskEntity>(); // задачи
        public double TotalHours => (Deadline - Start).TotalHours; // всего часов
        public double HoursSpent => Tasks.Sum(x => x.Hours); // потрачено на задачи
    }
}
