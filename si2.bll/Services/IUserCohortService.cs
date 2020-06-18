﻿using Microsoft.AspNetCore.JsonPatch;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Requests.UserCohort;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Dtos.Results.UserCohort;
//using si2.bll.Dtos.Results.UserCohort;
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
        //Task<UserCohort> AssignCohortsToUserAsync(Guid cohortId, Guid userId, CancellationToken ct);
        Task<UserCohortDto> AssignCohortsToUserAsync(String id, ManageCohortsUserDto addCohortsToUserDto, CancellationToken ct);
        Task<PagedList<UserCohortDto>> GetCohortsUserAsync(String userId, CancellationToken ct);
        Task DeleteCohortsUser(String userId, CancellationToken ct);
        Task<bool> ExistsAsync(String userId, CancellationToken ct);

    }
}
