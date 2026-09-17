namespace TaskManagerWebApi.Models.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string Login { get; set; }
        public string HashedPassword { get; set; } = null!;

        public ICollection<ProjectEntity> CreatedProjects { get; set; } = new List<ProjectEntity>();
        public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
        public ICollection<TaskEntity> CreatedTasks { get; set; } = new List<TaskEntity>();
        public ICollection<TaskEntity> AssignedTasks { get; set; } = new List<TaskEntity>();
    }
}
