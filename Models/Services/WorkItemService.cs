using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Exceptions;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Mappers;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services
{
    public class WorkItemService(ApplicationDbContext _context) : IWorkItemService
    {
        public async Task<IEnumerable<WorkItemResponse>> GetAllAsync(WorkItemFilter filter, CancellationToken cancellationToken = default)
        {            
            var query = _context.WorkItems
                .Where(x => filter.Status == null || x.Status == filter.Status)
                .Where(x => filter.Priority == null || x.Priority == filter.Priority)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var entities = await query.ToListAsync(cancellationToken);

            var responseList = new List<WorkItemResponse>();
            entities.ForEach(x => responseList.Add(WorkItemMapper.ToResponse(x)));

            return responseList;
        }

        public async Task<WorkItemResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var found = await GetEntityById(id, cancellationToken);
            return WorkItemMapper.ToResponse(found);
        }

        public async Task<WorkItemResponse> CreateAsync(CreateWorkItemRequest request, CancellationToken cancellationToken = default)
        {
            var entity = new WorkItem
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
            };

            await _context.WorkItems.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return WorkItemMapper.ToResponse(entity);
        }

        public async Task UpdateAsync(int id, UpdateWorkItemRequest request, CancellationToken cancellationToken = default)
        {
            var found = await GetByIdAsync(id, cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.Title)) found.Title = request.Title;
            if (!string.IsNullOrWhiteSpace(request.Description)) found.Description = request.Description;
            if (!string.IsNullOrWhiteSpace(request.Status)) found.Status = request.Status;
            if (!string.IsNullOrWhiteSpace(request.Priority)) found.Priority = request.Priority;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var found = await GetEntityById(id, cancellationToken);
            _context.WorkItems.Remove(found);

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<WorkItem> GetEntityById(int id, CancellationToken cancellationToken = default)
        {
            var found = await _context.WorkItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (found == null)
                throw new NotFoundException($"WorkItem not found. Id = {id}");

            return found;
        }
    }
}
    