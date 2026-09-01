namespace TaskManagerWebApi.Models.Requests
{
    public class UpdateTaskRequest : TaskRequestBase
    {
        public required string Status { get; set; }
    }
}
