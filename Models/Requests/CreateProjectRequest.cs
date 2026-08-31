namespace TaskManagerWebApi.Models.Requests
{
    public class CreateProjectRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int NumberOfHours { get; set; }
    }
}
