using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Results.Cohort;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Dtos.Results.UserCohort;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class UserCohortService : ServiceBase, IUserCohortService
    {
        public UserCohortService(IUnitOfWork uow, IMapper mapper, ILogger<IUserCohortService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<UserCohort> AssignUsersToCohortAsync(Guid cohortId, Guid userId, CancellationToken ct)
        {
            var userCohortEntity = new UserCohort();

            try
            {
                userCohortEntity.CohortId = cohortId;
                userCohortEntity.UserId = userId;
                await _uow.UserCohorts.AddAsync(userCohortEntity, ct);
                await _uow.SaveChangesAsync(ct);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return userCohortEntity;
        }

        public async Task<PagedList<UserCohortDto>> GetUsersCohortAsync(Guid cohortId, CancellationToken ct)
        {

            var userCohortEntities = _uow.UserCohorts.GetAllIncluding([uc =>uc.User]);

            var pagedListEntities = await PagedList<UserCohort>.CreateAsync(userCohortEntities,
                  1, userCohortEntities.Count(), ct);

            var result = _mapper.Map<PagedList<UserCohortDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;

            return result;
        }
    }
}
