namespace TaskManagerWebApi.Models.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string Login { get; set; }
    }
}
