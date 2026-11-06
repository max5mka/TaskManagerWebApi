namespace TaskManagerWebApi.Models.Entities
{
    public class RefreshTokenEntity
    {
        public int Id { get; set; }
        public required string TokenHash { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedAt { get; set; }

        public required int UserId { get; set; }
        public UserEntity User { get; set; } = null!;
    }
}
