namespace TaskManagerWebApi.Models.Constants
{
    public static class TaskStatuses
    {
        public const string New = "New";
        public const string InProgress = "In Progress";
        public const string Pending = "Pending";
        public const string Closed = "Closed";

        public static readonly IReadOnlySet<string> Allowed = new HashSet<string>
        {
            New,
            InProgress,
            Pending,
            Closed
        };
    }
}
