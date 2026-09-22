namespace TaskManagerWebApi.Models.Constants
{
    public static class ProjectRoles
    {
        public const string Owner = "Owner";
        public const string Developer = "Developer";
        public const string Observer = "Observer";

        public static readonly IReadOnlySet<string> Roles = new HashSet<string>
        {
            Owner,
            Developer,
            Observer,
        };
    }
}
