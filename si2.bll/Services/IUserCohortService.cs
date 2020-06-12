using Microsoft.AspNetCore.JsonPatch;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Dtos.Results.UserCohort;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IUserCohortService : IServiceBase
    {
        Task<UserCohort> AssignUsersToCohortAsync(Guid cohortId, Guid userId, CancellationToken ct);

        Task<PagedList<UserCohortDto>> GetUsersCohortAsync(Guid cohortId, CancellationToken ct);
        
    }
}
