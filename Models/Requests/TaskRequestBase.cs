using System.ComponentModel.DataAnnotations;
using TaskManagerWebApi.Models.Enums;

namespace TaskManagerWebApi.Models.Requests
{
    public class TaskRequestBase
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required double Hours { get; set; }
        public required PriorityEnum Priority { get; set; }
    }
}
