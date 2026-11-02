using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Data;
using TaskManagerWebApi.Exceptions;
using TaskManagerWebApi.Models.Constants;
using TaskManagerWebApi.Models.Entities;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Models.Services
{
    public class ProjectAccessService(
        ApplicationDbContext _context,
        ICurrentUserService _currentUserService) : IProjectAccessService
    {
        public async Task EnsureUserIsOwnerAsync(int projectId, CancellationToken cancellationToken = default)
        {
            var userProject = await EnsureUserIsMemberInternalAsync(projectId, cancellationToken);

            if (userProject.ProjectRole != ProjectRoles.Owner)
                throw new ForbiddenException("Only Owner can perform this action");
        }

        public async Task EnsureUserIsMemberAsync(int projectId, CancellationToken cancellationToken = default)
        {
            await EnsureUserIsMemberInternalAsync(projectId, cancellationToken);
        }

        private async Task<UserProject> EnsureUserIsMemberInternalAsync(
            int projectId,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var userProject = await _context.UserProjects
                .FirstOrDefaultAsync(
                    x => x.UserId == userId && x.ProjectId == projectId,
                    cancellationToken);

            if (userProject == null)
                throw new ForbiddenException("You are not a member of this project");

            return userProject;
        }
    }
}
