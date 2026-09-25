namespace TaskManagerWebApi.Models.Requests
{
    public class ProjectUpdateRequest : ProjectBaseRequest
    {
        public required string Status { get; set; }
    }
}
