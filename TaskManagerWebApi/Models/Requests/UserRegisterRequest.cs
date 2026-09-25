namespace TaskManagerWebApi.Models.Requests
{
    public class UserRegisterRequest : UserBaseRequest
    {
        public required string FirstName { get; set; }
    }
}
