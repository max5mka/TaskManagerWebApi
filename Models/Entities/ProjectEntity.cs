namespace TaskManagerWebApi.Models.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public required DateOnly Deadline { get; set; }
        public ICollection<WorkItemEntity> WorkItems = new List<WorkItemEntity>();
    }
}
