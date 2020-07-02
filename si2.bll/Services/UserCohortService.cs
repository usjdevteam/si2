using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.UserCohort;
using si2.bll.Dtos.Results.Cohort;
using si2.bll.Dtos.Results.UserCohort;
using si2.bll.Helpers.PagedList;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCohortService(IUnitOfWork uow, IMapper mapper, ILogger<IUserCohortService> logger, UserManager<ApplicationUser> userManager) : base(uow, mapper, logger)
        {
            _userManager = userManager;
        }

        public async Task<UserCohortDto> AssignCohortsToUserAsync(String id, ManageCohortsUserDto manageUsersCohortDto, CancellationToken ct)
        {
            UserCohortDto userCohortDto = null;

            var user = await _userManager.FindByIdAsync(id);
           
            if (manageUsersCohortDto.AddCohortsIds != null)
            {
                if (user.UserCohorts == null)
                    user.UserCohorts = new List<UserCohort>();

                foreach (var ac in manageUsersCohortDto.AddCohortsIds)
                {
                    var usersCohort = _uow.UserCohorts.FindBy(c => c.CohortId == ac && c.UserId == id).Count();

                    if (usersCohort == 0)
                    {
                        user.UserCohorts.Add(new UserCohort() { CohortId = ac, UserId = id });
                    }
                }
            }

            if (manageUsersCohortDto.DeleteCohortsIds != null)
            {
                foreach (var dc in manageUsersCohortDto.DeleteCohortsIds)
                {
                    var userCohort = _uow.UserCohorts.FindBy(c => c.CohortId == dc && c.UserId == id).FirstOrDefault();

                    if (userCohort != null)
                    {
                        _uow.UserCohorts.Delete(userCohort);
                    }
                }
            }

            await _uow.SaveChangesAsync(ct);

            /*var userCohortEntity = await _uow.UserCohorts.FindByAsync(c => c.UserId == id, ct); 

            userCohortDto = _mapper.Map<UserCohortDto>(userCohortEntity);*/

            return userCohortDto;
        }

        public async Task<PagedList<CohortDto>> GetCohortsUserAsync(String userId, CancellationToken ct)
        {
            var cohortsEntity = _uow.UserCohorts.GetAllIncluding(c => c.Cohort)
                                .Where(c => c.UserId == userId)
                                .Select(c => c.Cohort);

            if (cohortsEntity.Count() > 0)
            {
                var pagedListEntities = await PagedList<Cohort>.CreateAsync(cohortsEntity, 1, cohortsEntity.Count(), ct);

                var result = _mapper.Map<PagedList<CohortDto>>(pagedListEntities);
                result.TotalCount = pagedListEntities.TotalCount;
                result.TotalPages = pagedListEntities.TotalPages;
                result.CurrentPage = pagedListEntities.CurrentPage;
                result.PageSize = pagedListEntities.PageSize;

                return result;
            }
            else
            {
                return null;
            }

        }

        public async Task<bool> ExistsAsync(String userId, CancellationToken ct)
        {
            if (await _uow.UserCohorts.FirstAsync(c => c.UserId == userId, ct) != null)
                return true;

            return false;
        }
    }
}
