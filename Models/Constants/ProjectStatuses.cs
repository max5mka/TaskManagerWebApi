namespace TaskManagerWebApi.Models.Constants
{
    public static class ProjectStatuses
    {
        public const string New = "New";
        public const string InProgress = "In Progress";
        public const string Closed = "Closed";

        public static readonly IReadOnlySet<string> Allowed = new HashSet<string>
        {
            New,
            InProgress,
            Closed
        };
    }
}
