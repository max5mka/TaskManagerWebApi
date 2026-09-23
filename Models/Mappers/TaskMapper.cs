using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Mappers
{
    public static class TaskMapper
    {
        public static TaskEntity ToEntity(int userId, int projectId, TaskCreateRequest request) =>
            new TaskEntity
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                HoursToDo = request.HoursToDo,
                Deadline = request.Deadline,
                ProjectId = projectId,
                CreatorId = userId,
            };

        public static TaskCreateResponse ToCreateResponse(TaskEntity entity) =>
            new TaskCreateResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                Priority = entity.Priority,
                HoursToDo = entity.HoursToDo,
                Deadline = entity.Deadline,
                CreatedAt = entity.CreatedAt,
                ProjectId = entity.ProjectId,
                CreatorId = entity.CreatorId,
            };

        public static TaskShortResponse ToShortResponse(TaskEntity entity) =>
            new TaskShortResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Status = entity.Status,
                Priority = entity.Priority,
                HoursToDo = entity.HoursToDo,
                HoursLogged = entity.HoursLogged,
                TakenAt = entity.TakenAt,
                Deadline = entity.Deadline,
                CreatedAt = entity.CreatedAt,
                Creator = UserMapper.ToResponse(entity.Creator),
                Assignee = entity.Assignee == null ? null : UserMapper.ToResponse(entity.Assignee)
            };

        public static TaskLongResponse ToLongResponse(TaskEntity entity) =>
            new TaskLongResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                Priority = entity.Priority,
                HoursToDo = entity.HoursToDo,
                HoursLogged = entity.HoursLogged,
                TakenAt = entity.TakenAt,
                Deadline = entity.Deadline,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Creator = UserMapper.ToResponse(entity.Creator),
                Assignee = entity.Assignee == null ? null : UserMapper.ToResponse(entity.Assignee),
                Project = ProjectMapper.ToShortResponse(entity.Project)
            };

        public static List<TaskShortResponse> ToShortResponses(List<TaskEntity> entities)
        {
            var result = new List<TaskShortResponse>();
            entities.ForEach(x => result.Add(ToShortResponse(x)));
            return result;
        }
    }
}
