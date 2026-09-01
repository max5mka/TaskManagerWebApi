namespace TaskManagerWebApi.Models.Requests
{
    public abstract class ProjectRequestBase
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime Start { get; set; }
        public required DateTime Deadline { get; set; }
    }
}
