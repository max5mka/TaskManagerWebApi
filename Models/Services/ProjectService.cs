using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Exceptions;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services
{
    public class ProjectService(ApplicationDbContext _context) : IProjectService
    {
        private ProjectResponse ToResponse(ProjectEntity entity) =>
            new ProjectResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Status = entity.Status,
                Deadline = entity.Deadline,
            };


        public async Task<IEnumerable<ProjectResponse>> GetAllAsync(ProjectFilter filter, CancellationToken cancellationToken = default)
        {
            var query = _context.Projects
                .Where(x => filter.Status == null || x.Status == filter.Status)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var entities = await query.ToListAsync(cancellationToken);

            var responseList = new List<ProjectResponse>();
            entities.ForEach(x => responseList.Add(ToResponse(x)));

            return responseList;
        }


        public async Task<ProjectResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var found = await GetEntityById(id, cancellationToken);
            return ToResponse(found);
        }


        public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken = default)
        {
            var entity = new ProjectEntity
            {
                Title = request.Title,
                Description = request.Description,
                Status = "New",
                Deadline = request.Deadline,
            };

            await _context.Projects.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return ToResponse(entity);
        }


        public async Task<ProjectResponse> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken cancellationToken = default)
        {
            var found = await GetEntityById(id, cancellationToken);

            found.Title = request.Title;
            found.Description = request.Description;
            found.Status = request.Status;
            found.Deadline = request.Deadline;

            await _context.SaveChangesAsync(cancellationToken);
            return ToResponse(found);
        }


        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var found = await GetEntityById(id, cancellationToken);
            _context.Projects.Remove(found);

            await _context.SaveChangesAsync(cancellationToken);
        }


        private async Task<ProjectEntity> GetEntityById(int id, CancellationToken cancellationToken = default)
        {
            await EnsureProjectExistsAsync(id, cancellationToken);
            return await _context.Projects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        
        public async Task EnsureProjectExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            var allProjects = _context.Projects.ToList();
            var allIds = _context.Projects.Select(x => x.Id).ToList();

            if (!await _context.Projects.AnyAsync(p => p.Id == id, cancellationToken))
            {
                throw new NotFoundException($"Project with id={id} not found.");
            }
        }
    }
}
