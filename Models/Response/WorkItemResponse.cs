namespace TaskManagerWebApi.Models.Response
{
    public class WorkItemResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required string Priority { get; set; }
    }
}
