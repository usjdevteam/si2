using si2.bll.Dtos.Requests.UserCohort;
using si2.bll.Dtos.Results.Cohort;
using si2.bll.Dtos.Results.UserCohort;
using si2.bll.Helpers.PagedList;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IUserCohortService : IServiceBase
    {
        Task<UserCohortDto> AssignCohortsToUserAsync(String id, ManageCohortsUserDto addCohortsToUserDto, CancellationToken ct);
        Task<PagedList<CohortDto>> GetCohortsUserAsync(String userId, CancellationToken ct);
        Task<bool> ExistsAsync(String userId, CancellationToken ct);
    }
}
