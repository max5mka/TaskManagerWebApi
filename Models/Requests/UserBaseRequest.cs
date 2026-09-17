namespace TaskManagerWebApi.Models.Requests
{
    public abstract class UserBaseRequest
    {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
