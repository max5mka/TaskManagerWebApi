using System.Security.Claims;
using TaskManagerWebApi.Exceptions;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public class CurrentUserService(IHttpContextAccessor _accessor) : ICurrentUserService
    {
        public int UserId
        {
            get
            {
                var claim = _accessor.HttpContext?.User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(claim))
                    throw new UnauthorizedException("User ID not found.");

                return int.Parse(claim);
            }
        }

        public void EnsureUserExists()
        {
            _ = UserId;
        }
    }
}
