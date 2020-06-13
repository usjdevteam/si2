using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Results.Administration;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public CohortService(IUnitOfWork uow, IMapper mapper, ILogger<ICohortService> logger, UserManager<ApplicationUser> userManager) : base(uow, mapper, logger)
        {
            _userManager = userManager;
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

        public async Task AssignUsersToCohortAsync(Guid id, AddUsersToCohortDto addUsersToCohortDto, CancellationToken ct)
        {

            var users = await _userManager.Users.Where(c => addUsersToCohortDto.UsersIds.Any(u => u == c.Id)).ToListAsync(ct);

            var cohortEntity = await _uow.Cohorts.GetAsync(id, ct);

            foreach (var userid in addUsersToCohortDto.UsersIds)
            {
                //_uow.UserCohorts.Add(new UserCohort() { CohortId = id, UserId = new Guid(userid) });
                _uow.UserCohorts.Add(new UserCohort() { CohortId = id, UserId = userid });
            }

            await _uow.SaveChangesAsync(ct);
        }


        public async Task<PagedList<UserDto>> GetUsersCohortAsync(Guid cohortId, CancellationToken ct)
        {
            var cohortUsersIds = await _uow.UserCohorts.FindByAsync(c => c.CohortId == cohortId, ct); ;
            
            var usersIds = cohortUsersIds.Select(c => c.UserId);

            var usersEntity = _userManager.Users.Where(user => usersIds.Contains(user.Id));


            var pagedListEntities = await PagedList<ApplicationUser>.CreateAsync(usersEntity, 1, usersEntity.Count(), ct);

            var result = _mapper.Map<PagedList<UserDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;

            return result;



            //var users = cohortUsersIds.Where(c => c.CohortId == cohortId).Select(c => c.User);

            //var cohortUsersIds = await _uow.UserCohorts.GetAll().ToListAsync();
            // var users = cohortUsersIds.Where(c=> c.CohortId == cohortId).Select(c=>c.User);
            // -> delete var users = await _userManager.Users.Where(c => addUsersToCohortDto.UsersIds.Any(u => u == c.Id)).ToListAsync(ct);

            
        }

    }
}
