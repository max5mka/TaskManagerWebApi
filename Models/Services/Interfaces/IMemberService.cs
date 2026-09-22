using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;

namespace TaskManagerWebApi.Models.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberResponse>> GetAllAsync(int projectId, UserFilter filter, CancellationToken cancellationToken = default);
        Task AddAsync(int projectId, MemberAddRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(int projectId, int memberId, CancellationToken cancellationToken = default);
    }
}
