using TaskManagerWebApi.Models.Enums;

namespace TaskManagerWebApi.Models.Response
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required PriorityEnum Priority { get; set; }
        public required double Hours { get; set; }
    }
}
