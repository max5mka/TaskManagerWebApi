namespace TaskManagerWebApi.Models.Requests
{
    public class MemberAddByLoginRequest
    {
        public required string MemberLogin { get; set; }
        public required string Role { get; set; }
    }
}
