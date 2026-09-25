namespace TaskManagerWebApi.Models.Response
{
    public class TaskShortResponse
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
        public required double HoursToDo { get; set; }
        public required double HoursLogged { get; set; }
        public required DateTime? TakenAt { get; set; }
        public required DateTime Deadline { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required UserResponse Creator { get; set; }
        public required UserResponse? Assignee { get; set; }
    }
}
