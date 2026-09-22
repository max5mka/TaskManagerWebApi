using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Data;
using TaskManagerWebApi.Exceptions;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Mappers;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;
using TaskManagerWebApi.Models.Services.Interfaces;
using static System.Net.WebRequestMethods;

namespace TaskManagerWebApi.Models.Services
{
    public class MemberService(
        ApplicationDbContext _context,
        IProjectService _projectService,
        ICurrentUserService _currentUserService) : IMemberService
    {
        public async Task<IEnumerable<MemberResponse>> GetAllAsync(int projectId, UserFilter filter, CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId;
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var query = _context.UserProjects
                .Include(x => x.User)
                .Where(x => x.ProjectId == projectId)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var entities = await query.ToListAsync(cancellationToken);
            return MemberMapper.ToResponses(entities);
        }

        public async Task AddAsync(int projectId, MemberAddRequest request, CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId;
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var added = await _context.UserProjects.FirstOrDefaultAsync(x => x.UserId == request.MemberId && x.ProjectId == projectId);
            if (added != null)
                throw new AlreadyExistsException($"Member with id={request.MemberId} already exists in project with id={projectId}");

            added = new UserProject
            {
                UserId = request.MemberId,
                ProjectId = projectId,
                ProjectRole = request.Role
            };

            _context.UserProjects.Add(added);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int projectId, int memberId, CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId;
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);

            var deleted = await _context.UserProjects.FirstOrDefaultAsync(x => x.UserId == memberId && x.ProjectId == projectId);
            if (deleted == null)
                throw new NotFoundException($"Member wit id={memberId} not found");

            _context.UserProjects.Remove(deleted);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
