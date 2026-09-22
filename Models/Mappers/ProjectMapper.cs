using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectEntity ToEntity(int userId, ProjectCreateRequest request) =>
            new()
            {
                Title = request.Title,
                Description = request.Description,
                Status = Statuses.New,
                Start = request.Start,
                Deadline = request.Deadline,
                CreatorId = userId,
                UserProjects = new List<UserProject> { new UserProject { UserId = userId, ProjectRole = ProjectRoles.Owner }, }
            };

        public static ProjectCreateResponse ToCreateResponse(ProjectEntity entity) =>
            new()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                Start = entity.Start,
                DeadLine = entity.Deadline,
                CreatedAt = entity.CreatedAt,
                TotalHours = entity.TotalHours,
            };

        public static ProjectShortResponse ToShortResponse(ProjectEntity entity) =>
            new()
            {
                Id = entity.Id,
                Title = entity.Title,
                Status = entity.Status,
                Start = entity.Start,
                DeadLine = entity.Deadline,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Creator = UserMapper.ToResponse(entity.Creator),
            };

        public static ProjectLongResponse ToLongResponse(ProjectEntity entity) =>
            new()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                Start = entity.Start,
                DeadLine = entity.Deadline,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                TotalHours = entity.TotalHours,
                HoursSpent = entity.HoursSpent,
                Creator = UserMapper.ToResponse(entity.Creator),
                Members = MemberMapper.ToResponses(entity.UserProjects.ToList()),
                Tasks = TaskMapper.ToShortResponses(entity.Tasks.ToList())
            };


        public static List<ProjectShortResponse> ToShortResponses(List<ProjectEntity> entities)
        {
            var result = new List<ProjectShortResponse>();
            entities.ForEach(x => result.Add(ToShortResponse(x)));
            return result;
        }
    }
}
