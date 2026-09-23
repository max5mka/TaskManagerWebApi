using Azure.Core;
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
    public class MemberService(
        ApplicationDbContext _context,
        IProjectService _projectService,
        IUserService _userService,
        ICurrentUserService _currentUserService) : IMemberService
    {
        public async Task<IEnumerable<MemberResponse>> GetAllAsync(int projectId, UserFilter filter, CancellationToken cancellationToken = default)
        {
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);
            await EnsureUserIsMemberAsync(projectId, cancellationToken);

            var query = _context.UserProjects
                .Include(x => x.User)
                .Where(x => x.ProjectId == projectId)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var members = await query.ToListAsync(cancellationToken);
            return MemberMapper.ToResponses(members);
        }

        public async Task AddAsync(int projectId, MemberAddRequest request, CancellationToken cancellationToken = default)
        {
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);
            await EnsureUserIsOwnerAsync(projectId, cancellationToken);
            await _userService.EnsureUserExistsAsync(request.MemberId, cancellationToken);

            var existing = await _context.UserProjects.FirstOrDefaultAsync(x => x.UserId == request.MemberId && x.ProjectId == projectId);
            if (existing != null)
                throw new AlreadyExistsException($"This user is already a member");

            var newMember = new UserProject
            {
                UserId = request.MemberId,
                ProjectId = projectId,
                ProjectRole = request.Role
            };

            _context.UserProjects.Add(newMember);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int projectId, int memberId, CancellationToken cancellationToken = default)
        {
            await _projectService.EnsureProjectExistsAsync(projectId, cancellationToken);
            await EnsureUserIsOwnerAsync(projectId, cancellationToken);

            var deleting = await _context.UserProjects.FirstOrDefaultAsync(x => x.UserId == memberId && x.ProjectId == projectId);
            if (deleting == null)
                throw new NotFoundException($"There is no such user among the members");

            if (_currentUserService.UserId == memberId)
                throw new ForbiddenException("You cannot delete yourself");

            _context.UserProjects.Remove(deleting);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task EnsureUserIsOwnerAsync(int projectId, CancellationToken cancellationToken)
        {
            var userProject = await EnsureUserIsMemberAsync(projectId, cancellationToken);

            if (userProject.ProjectRole != ProjectRoles.Owner)
                throw new ForbiddenException("Only Owner can perform this action");
        }

        private async Task<UserProject> EnsureUserIsMemberAsync(int projectId, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var userProject = await _context.UserProjects
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProjectId == projectId, cancellationToken);

            if (userProject == null)
                throw new ForbiddenException("You are not a member of this project");

            return userProject;
        }
    }
}
