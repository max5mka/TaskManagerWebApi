namespace TaskManagerWebApi.Models.Requests
{
    public class TaskBaseRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required double HoursToDo { get; set; }
        public required string Priority { get; set; }
        public required DateTime Deadline { get; set; }
    }
}
