using TaskManagerWebApi.Models.Entities;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IJWTService
    {
        string GenerateToken(UserEntity user);
    }
}
