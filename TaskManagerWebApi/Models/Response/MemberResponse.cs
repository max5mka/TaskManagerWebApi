namespace TaskManagerWebApi.Models.Response
{
    public class MemberResponse
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string Login { get; set; }
        public required string ProjetRole { get; set; }
    }
}
