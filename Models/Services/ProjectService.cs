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
    public class ProjectService(
        ApplicationDbContext _context,
        IUserService _userService) : IProjectService
    {
        public async Task<IEnumerable<ProjectShortResponse>> GetAllAsync(int userId, ProjectFilter filter, CancellationToken cancellationToken = default)
        {
            await _userService.EnsureUserExistsAsync(userId, cancellationToken);

            var query = _context.Projects
                .Include(p => p.Tasks)
                .Include(p => p.Creator)
                .Where(p => p.UserProjects.Any(up => up.UserId == userId))
                .Where(p => filter.Status == null || p.Status == filter.Status)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var entities = await query.ToListAsync(cancellationToken);
            return ProjectMapper.ToShortResponses(entities);
        }


        public async Task<ProjectLongResponse> GetByIdAsync(int userId, int projectId, CancellationToken cancellationToken = default)
        {
            var found = await GetEntityWithDetailsAsync(userId, projectId, cancellationToken);
            return ProjectMapper.ToLongResponse(found);
        }


        public async Task<ProjectCreateResponse> CreateAsync(int userId, ProjectCreateRequest request, CancellationToken cancellationToken = default)
        {
            await _userService.EnsureUserExistsAsync(userId, cancellationToken);

            var newProject = ProjectMapper.ToEntity(userId, request);

            await _context.Projects.AddAsync(newProject, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return ProjectMapper.ToCreateResponse(newProject);
        }


        public async Task<ProjectLongResponse> UpdateAsync(int userId, int projectId, ProjectUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var found = await GetEntityWithDetailsAsync(userId, projectId, cancellationToken);

            if (!string.Equals(found.Title, request.Title)
                || !string.Equals(found.Description, request.Description)
                || !string.Equals(found.Status, request.Status)
                || !string.Equals(found.Start, request.Start)
                || !string.Equals(found.Deadline, request.Deadline))
            {
                found.UpdatedAt = DateTime.UtcNow;
            }

            found.Title = request.Title;
            found.Description = request.Description;
            found.Status = request.Status;
            found.Start = request.Start;
            found.Deadline = request.Deadline;

            await _context.SaveChangesAsync(cancellationToken);
            return ProjectMapper.ToLongResponse(found);
        }


        public async Task DeleteAsync(int userId, int projectId, CancellationToken cancellationToken = default)
        {
            var found = await GetEntityAsync(userId, projectId, cancellationToken);
            _context.Projects.Remove(found);

            await _context.SaveChangesAsync(cancellationToken);
        }


        private async Task<ProjectEntity> GetEntityWithDetailsAsync(int userId, int projectId, CancellationToken cancellationToken = default)
        {
            await _userService.EnsureUserExistsAsync(userId, cancellationToken);
            await EnsureProjectExistsAsync(userId, projectId, cancellationToken);

            return await _context.Projects
                .Include(p => p.Tasks)
                .Include(p => p.UserProjects)
                    .ThenInclude(up => up.User)
                .FirstOrDefaultAsync(x => x.Id == projectId, cancellationToken);
        }

        private async Task<ProjectEntity> GetEntityAsync(int userId, int projectId, CancellationToken cancellationToken = default)
        {
            await _userService.EnsureUserExistsAsync(userId, cancellationToken);
            await EnsureProjectExistsAsync(userId, projectId, cancellationToken);

            return await _context.Projects
                .FirstOrDefaultAsync(x => x.Id == projectId, cancellationToken);
        }


        public async Task EnsureProjectExistsAsync(int userId, int projectId, CancellationToken cancellationToken = default)
        {
            await _userService.EnsureUserExistsAsync(userId, cancellationToken);

            if (!await _context.Projects.AnyAsync(p => p.Id == projectId, cancellationToken))
            {
                throw new NotFoundException($"Project with id={projectId} not found.");
            }
        }
    }
}
