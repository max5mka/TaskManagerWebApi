namespace TaskManagerWebApi.Models.Requests
{
    public class TaskUpdateRequest : TaskBaseRequest
    {
        public required string Status { get; set; }
        public required double HoursLogged { get; set; }
        public required int? AssigneeId { get; set; }
    }
}
