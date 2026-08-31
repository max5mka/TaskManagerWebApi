using Azure.Core;
using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Exceptions;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Models.Services
{
    public class TaskService(
        ApplicationDbContext _context,
        IProjectService _projectService) : ITaskService
    {
        private TaskResponse ToResponse(TaskEntity entity) =>
            new TaskResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                Priority = entity.Priority,
                NumberOfHours = entity.NumberOfHours,
            };


        public async Task<IEnumerable<TaskResponse>> GetAllAsync(
            int projectId,
            TaskFilter filter, 
            CancellationToken cancellationToken = default)
        {
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var query = _context.Tasks
                .Where(x => x.ProjectId == projectId)
                .Where(x => filter.Status == null || x.Status == filter.Status)
                .Where(x => filter.Priority == null || x.Priority == filter.Priority)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var entities = await query.ToListAsync(cancellationToken);

            var responseList = new List<TaskResponse>();
            entities.ForEach(x => responseList.Add(ToResponse(x)));

            return responseList;
        }


        public async Task<TaskResponse> GetByIdAsync(
            int projectId,
            int taskId, 
            CancellationToken cancellationToken = default)
        {
            var found = await GetEntityById(projectId, taskId, cancellationToken);
            return ToResponse(found);
        }


        public async Task<TaskResponse> CreateAsync(
            int projectId, 
            CreateTaskRequest request, 
            CancellationToken cancellationToken = default)
        {
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var entity = new TaskEntity
            {
                Title = request.Title,
                Description = request.Description,
                Status = "New",
                Priority = request.Priority,
                NumberOfHours = request.NumberOfHours,
                ProjectId = projectId
            };

            var add = await _context.Tasks.AddAsync(entity, cancellationToken);
            var save = await _context.SaveChangesAsync(cancellationToken);
            return ToResponse(entity);
        }


        public async Task<TaskResponse> UpdateAsync(
            int projectId,
            int taskId, 
            UpdateTaskRequest request, 
            CancellationToken cancellationToken = default)
        {
            var found = await GetEntityById(projectId, taskId, cancellationToken);
            found.Title = request.Title;
            found.Description = request.Description;
            found.Status = request.Status;
            found.Priority = request.Priority;

            await _context.SaveChangesAsync(cancellationToken);
            return ToResponse(found);
        }


        public async Task DeleteAsync(
            int projectId,
            int taskId, 
            CancellationToken cancellationToken = default)
        {
            var found = await GetEntityById(projectId, taskId, cancellationToken);
            _context.Tasks.Remove(found);

            await _context.SaveChangesAsync(cancellationToken);
        }


        private async Task<TaskEntity> GetEntityById(
            int projectId, 
            int taskId, 
            CancellationToken cancellationToken = default)
        {
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var found = await _context.Tasks
                .FirstOrDefaultAsync(x => x.ProjectId == projectId && x.Id == taskId, cancellationToken);

            if (found == null)
                throw new NotFoundException($"Task with id={taskId} not found.");

            return found;
        }
    }
}
    