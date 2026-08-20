namespace TaskManagerWebApi.Models.DTOs
{
    public class WorkItemFilterDTO
    {
        public string? Status { get; set; } = null;
        public string? Priority { get; set; } = null;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
