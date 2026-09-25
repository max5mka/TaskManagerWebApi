namespace TaskManagerWebApi.Models.Filters
{
    public class ProjectFilter : FilterBase
    {
        public string? Status { get; set; } = null;
    }
}
