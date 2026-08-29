using TaskManagerWebApi.Models.Entities;

namespace TaskManagerWebApi.Models.Response
{
    public class ProjectResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required DateOnly Deadline { get; set; }
    }
}
