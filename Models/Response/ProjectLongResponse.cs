using TaskManagerWebApi.Models.Entities;

namespace TaskManagerWebApi.Models.Response
{
    public class ProjectLongResponse
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required DateTime Start { get; set; }
        public required DateTime DeadLine { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
        public required double TotalHours { get; set; }
        public required double HoursSpent { get; set; }
        public required UserResponse Creator { get; set; }
        public required List<UserResponse> Members { get; set; }
        public required List<TaskShortResponse> Tasks { get; set; }
    }
}
