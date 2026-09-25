using TaskManagerWebApi.Models.Enums;

namespace TaskManagerWebApi.Models.Filters
{
    public class TaskFilter : FilterBase
    {
        public string? Status { get; set; } = null;
        public string? Priority { get; set; } = null;
    }
}
