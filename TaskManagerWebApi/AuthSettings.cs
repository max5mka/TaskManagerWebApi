namespace TaskManagerWebApi
{
    public class AuthSettings
    {
        public required TimeSpan Expires { get; set; }
        public TimeSpan RefreshTokenExpires { get; set; } = TimeSpan.FromDays(7);
        public required string SecretKey { get; set; }
    }
}
