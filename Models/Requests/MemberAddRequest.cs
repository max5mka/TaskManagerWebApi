namespace TaskManagerWebApi.Models.Requests
{
    public class MemberAddRequest
    {
        public required int MemberId { get; set; }
        public required string Role { get; set; }
    }
}
