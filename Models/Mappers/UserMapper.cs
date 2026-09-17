using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Mappers
{
    public static class UserMapper
    {
        public static UserResponse ToResponse(UserEntity entity) =>
            new()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                Login = entity.Login,
            };

        public static List<UserResponse> ToResponses(List<UserEntity> entities)
        {
            var result = new List<UserResponse>();
            entities.ForEach(x => result.Add(ToResponse(x)));
            return result;
        }
    }
}
