using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Results.Cohort;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using Si2.common.Exceptions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static si2.common.Enums;

namespace si2.bll.Services
{
    public class CohortService : ServiceBase, ICohortService
    {
        public CohortService(IUnitOfWork uow, IMapper mapper, ILogger<ICohortService> logger) : base(uow, mapper, logger)
        {
        }

        public async Task<CohortDto> CreateCohortAsync(CreateCohortDto createCohortDto, CancellationToken ct)
        {
            CohortDto cohortDto = null;
            try
            {
                var cohortEntity = _mapper.Map<Cohort>(createCohortDto);
                await _uow.Cohorts.AddAsync(cohortEntity, ct);
                await _uow.SaveChangesAsync(ct);
                cohortDto = _mapper.Map<CohortDto>(cohortEntity);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);

            }
            return cohortDto;
        }

        public async Task<CohortDto> GetCohortByIdAsync(Guid id, CancellationToken ct)
        {
            CohortDto cohortDto = null;

            var cohortEntity = await _uow.Cohorts.GetAsync(id, ct);
            if (cohortEntity != null)
            {
                cohortDto = _mapper.Map<CohortDto>(cohortEntity);
            }

            return cohortDto;
        }

        public async Task<PagedList<CohortDto>> GetCohortsAsync(CancellationToken ct)
        {
            var cohortEntities = _uow.Cohorts.GetAll();

            var pagedListEntities = await PagedList<Cohort>.CreateAsync(cohortEntities,
                  1, cohortEntities.Count(), ct);

            var result = _mapper.Map<PagedList<CohortDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;

            return result;
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        {
            if (await _uow.Cohorts.GetAsync(id, ct) != null)
                return true;

            return false;
        }


    }
}
