using TaskManagerWebApi.Models.Entities;

namespace TaskManagerWebApi.Models.Response
{
    public class TaskCreateResponse
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
        public required double HoursToDo { get; set; }
        public required DateTime Deadline { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required int ProjectId { get; set; }
        public required int CreatorId { get; set; }
    }
}
