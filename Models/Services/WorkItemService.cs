using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Exceptions;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Models.Services
{
    public class WorkItemService(
        ApplicationDbContext _context,
        IProjectService _projectService) : IWorkItemService
    {
        private WorkItemResponse ToResponse(WorkItemEntity entity) =>
            new WorkItemResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                Priority = entity.Priority,
            };


        public async Task<IEnumerable<WorkItemResponse>> GetAllAsync(
            int projectId,
            WorkItemFilter filter, 
            CancellationToken cancellationToken = default)
        {
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var query = _context.WorkItems
                .Where(x => x.ProjectId == projectId)
                .Where(x => filter.Status == null || x.Status == filter.Status)
                .Where(x => filter.Priority == null || x.Priority == filter.Priority)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var entities = await query.ToListAsync(cancellationToken);

            var responseList = new List<WorkItemResponse>();
            entities.ForEach(x => responseList.Add(ToResponse(x)));

            return responseList;
        }


        public async Task<WorkItemResponse> GetByIdAsync(
            int projectId,
            int workItemId, 
            CancellationToken cancellationToken = default)
        {
            var found = await GetEntityById(projectId, workItemId, cancellationToken);
            return ToResponse(found);
        }


        public async Task<WorkItemResponse> CreateAsync(
            int projectId, 
            CreateWorkItemRequest request, 
            CancellationToken cancellationToken = default)
        {
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var entity = new WorkItemEntity
            {
                Title = request.Title,
                Description = request.Description,
                Status = "New",
                Priority = request.Priority,
                ProjectId = projectId
            };

            var add = await _context.WorkItems.AddAsync(entity, cancellationToken);
            var save = await _context.SaveChangesAsync(cancellationToken);
            return ToResponse(entity);
        }


        public async Task<WorkItemResponse> UpdateAsync(
            int projectId,
            int workItemId, 
            UpdateWorkItemRequest request, 
            CancellationToken cancellationToken = default)
        {
            var found = await GetEntityById(projectId, workItemId, cancellationToken);
            found.Title = request.Title;
            found.Description = request.Description;
            found.Status = request.Status;
            found.Priority = request.Priority;

            await _context.SaveChangesAsync(cancellationToken);
            return ToResponse(found);
        }


        public async Task DeleteAsync(
            int projectId,
            int workItemId, 
            CancellationToken cancellationToken = default)
        {
            var found = await GetEntityById(projectId, workItemId, cancellationToken);
            _context.WorkItems.Remove(found);

            await _context.SaveChangesAsync(cancellationToken);
        }


        private async Task<WorkItemEntity> GetEntityById(
            int projectId, 
            int workItemId, 
            CancellationToken cancellationToken = default)
        {
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var found = await _context.WorkItems
                .FirstOrDefaultAsync(x => x.ProjectId == projectId && x.Id == workItemId, cancellationToken);

            if (found == null)
                throw new NotFoundException($"WorkItem with id={workItemId} not found.");

            return found;
        }
    }
}
    