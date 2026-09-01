namespace TaskManagerWebApi.Models.Requests
{
    public class UpdateProjectRequest : ProjectRequestBase
    {
        public required string Status { get; set; }
    }
}
