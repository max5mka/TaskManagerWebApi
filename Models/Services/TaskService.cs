using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Data;
using TaskManagerWebApi.Exceptions;
using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Mappers;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Models.Services
{
    public class TaskService(
        ApplicationDbContext _context,
        IProjectService _projectService,
        ICurrentUserService _currentUserService) : ITaskService
    {
        public async Task<IEnumerable<TaskShortResponse>> GetAllAsync(
            int projectId,
            TaskFilter filter, 
            CancellationToken cancellationToken = default)
        {
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var query = _context.Tasks
                .Include(x => x.Creator)
                .Include(x => x.Assignee)
                .Where(x => x.ProjectId == projectId)
                .Where(x => filter.Status == null || x.Status == filter.Status)
                .Where(x => filter.Priority == null || x.Priority == filter.Priority)
                .OrderBy(x => x.Id)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var entities = await query.ToListAsync(cancellationToken);
            return TaskMapper.ToShortResponses(entities);
        }


        public async Task<TaskLongResponse> GetByIdAsync(
            int projectId,
            int taskId, 
            CancellationToken cancellationToken = default)
        {
            var found = await GetEntity(projectId, taskId, cancellationToken);
            return TaskMapper.ToLongResponse(found);
        }


        public async Task<TaskCreateResponse> CreateAsync(
            int projectId, 
            TaskCreateRequest request, 
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId;
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var entity = TaskMapper.ToEntity(userId, projectId, request);

            await _context.Tasks.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return TaskMapper.ToCreateResponse(entity);
        }


        public async Task<TaskLongResponse> UpdateAsync(
            int projectId,
            int taskId, 
            TaskUpdateRequest request, 
            CancellationToken cancellationToken = default)
        {
            var found = await GetEntity(projectId, taskId, cancellationToken);

            if (!string.Equals(found.Title, request.Title)
                || !string.Equals(found.Description, request.Description)
                || !string.Equals(found.Status, request.Status)
                || !string.Equals(found.Priority, request.Priority)
                || !string.Equals(found.HoursToDo, request.HoursToDo)
                || !string.Equals(found.HoursLogged, request.HoursLogged)
                || !string.Equals(found.AssigneeId, request.AssigneeId))
            {
                found.UpdatedAt = DateTime.UtcNow;
            }

            found.Title = request.Title;
            found.Description = request.Description;
            found.Status = request.Status;
            found.Priority = request.Priority;
            found.HoursToDo = request.HoursToDo;
            found.HoursLogged = request.HoursLogged;
            found.AssigneeId = request.AssigneeId;

            if (request.AssigneeId != null)
            {
                found.TakenAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return TaskMapper.ToLongResponse(found);
        }


        public async Task DeleteAsync(
            int projectId,
            int taskId, 
            CancellationToken cancellationToken = default)
        {
            var found = await GetEntity(projectId, taskId, cancellationToken);
            _context.Tasks.Remove(found);

            await _context.SaveChangesAsync(cancellationToken);
        }


        private async Task<TaskEntity> GetEntity(
            int projectId, 
            int taskId, 
            CancellationToken cancellationToken = default)
        {
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var found = await _context.Tasks
                .Include(x => x.Creator)
                .Include(x => x.Project)
                .Include(x => x.Assignee)
                .FirstOrDefaultAsync(x => x.ProjectId == projectId && x.Id == taskId, cancellationToken);

            if (found == null)
                throw new NotFoundException($"Task with id={taskId} not found.");

            return found;
        }
    }
}
    