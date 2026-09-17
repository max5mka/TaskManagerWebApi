namespace TaskManagerWebApi.Models.Entities
{
    public class UserProject
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public int ProjectId { get; set; }
        public UserEntity User { get; set; } = null!;
        public ProjectEntity Project { get; set; } = null!;
    }
}
