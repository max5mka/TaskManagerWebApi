using Microsoft.EntityFrameworkCore.Query.Internal;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Mappers
{
    public class MemberMapper
    {
        public static MemberResponse ToResponse(UserProject entity) =>
            new()
            {
                Id = entity.UserId,
                FirstName = entity.User.FirstName,
                Login = entity.User.Login,
                ProjetRole = entity.ProjectRole
            };

        public static List<MemberResponse> ToResponses(List<UserProject> entities)
        {
            var result = new List<MemberResponse>();
            entities.ForEach(x => result.Add(ToResponse(x)));
            return result;
        }
    }
}
