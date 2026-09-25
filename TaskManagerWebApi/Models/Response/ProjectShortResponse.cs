namespace TaskManagerWebApi.Models.Response
{
    public class ProjectShortResponse
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Status { get; set; }
        public required DateTime Start { get; set; }
        public required DateTime DeadLine { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
        public required UserResponse Creator { get; set; }
    }
}
