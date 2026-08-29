using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebApi.Models.Requests
{
    public class CreateWorkItemRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Priority { get; set; }
    }
}
