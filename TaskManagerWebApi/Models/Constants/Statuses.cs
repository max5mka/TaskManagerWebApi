namespace TaskManagerWebApi.Models.Constants
{
    public static class Statuses
    {
        public const string New = "New";
        public const string InProgress = "In Progress";
        public const string Pending = "Pending";
        public const string Interrupted = "Interrupted";
        public const string Closed = "Closed";

        public static readonly IReadOnlySet<string> TaskAllowed = new HashSet<string>
        {
            New,
            InProgress,
            Pending,
            Interrupted,
            Closed
        };

        public static readonly IReadOnlySet<string> ProjectAllowed = new HashSet<string>
        {
            New,
            InProgress,
            Closed
        };
    }
}
