using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
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
    public class CourseCohortService : ServiceBase, ICourseCohortService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseCohortService(IUnitOfWork uow, IMapper mapper, ILogger<IUserCourseService> logger, UserManager<ApplicationUser> userManager) : base(uow, mapper, logger)
        {
            _userManager = userManager;
        }

        /*public async Task<UserCohort> AssignUsersToCohortAsync(Guid cohortId, Guid userId, CancellationToken ct)
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
        }*/

        /*public async Task<UserCohortDto> AssignCohortsToUserAsync(String id, ManageCohortsUserDto manageUsersCohortDto, CancellationToken ct)
        {
            UserCohortDto userCohortDto = null;

            var user = await _userManager.FindByIdAsync(id);
            /*foreach(var uc in user.UserCohorts)
            {
                if (!addUsersToCohortDto.CohortsIds.Any(c => uc.CohortId == c))
                    user.UserCohorts.Remove(uc);
            }*/

        /*      if (manageUsersCohortDto.AddCohortsIds != null)
              {
                  foreach (var ac in manageUsersCohortDto.AddCohortsIds)
                  {
                      if (!user.UserCohorts.Any(c => ac == c.CohortId))
                          user.UserCohorts.Add(new UserCohort() { CohortId = ac, UserId = id });
                  }
              }

              if (manageUsersCohortDto.DeleteCohortsIds != null)
              {
                  foreach (var dc in manageUsersCohortDto.DeleteCohortsIds)
                  {
                      var userCohort = user.UserCohorts.FirstOrDefault(c => c.CohortId == dc && c.UserId == id);
                      //var userCohort = user.UserCohorts
                      //.Where(c => c.CohortId == dc && c.UserId == id)
                      //.SingleOrDefault();

                      //if (user.UserCohorts.Any(c => ac == c.CohortId))
                      if (userCohort != null)
                      {
                          user.UserCohorts.Remove(userCohort);
                      }
                  }
              }

              /*var users = await _userManager.Users
                  .Where(c => addUsersToCohortDto.CohortsIds.Any(u => u == c.Id)).ToListAsync(ct);

              foreach (var cohortid in addUsersToCohortDto.CohortsIds)
              {
                  _uow.UserCohorts.Add(new UserCohort() { UserId = id, CohortId = new Guid(cohortid) });
              }*/

        /*       await _uow.SaveChangesAsync(ct);

               /*var userCohortEntity = await _uow.UserCohorts.FindByAsync(c => c.UserId == id, ct); 

               userCohortDto = _mapper.Map<UserCohortDto>(userCohortEntity);*/

        /*          return userCohortDto;
              }

              public async Task<PagedList<UserCohortDto>> GetCohortsUserAsync(String userId, CancellationToken ct)
              {

                  var cohortUsersIds = await _uow.UserCohorts.FindByAsync(c => c.UserId == userId, ct);

                  //var cohortsIds = cohortUsersIds.Select(c => c.CohortId);

                  //var cohortsEntity = Cohort.Where(cohort => cohortsIds.Contains(cohort.Id));

                  //var cohortsEntity = await _uow.Cohorts.FindByAsync(cohort => cohortsIds.Contains(cohort.Id), ct);
                  //var cohortsEntityList = cohortsEntity.ToList();

                  //var cohorts = await _uow.UserCohorts.FindByAsync(c => c.UserId == userId, ct).Include(e => e.Cohorts).ToList();

                  var cohortsEntity = _userManager.Users
                      .Include(u => u.UserCohorts)
                      .ThenInclude(uc => uc.Cohort)
                      .Where(c => c.Id == userId).ToList();

                  /*var pagedListEntities = await PagedList<ApplicationUser>.CreateAsync(cohortsEntity, 1, cohortsEntity.Count(), ct);

                  var result = _mapper.Map<PagedList<UserCohortDto>>(pagedListEntities);
                  result.TotalCount = pagedListEntities.TotalCount;
                  result.TotalPages = pagedListEntities.TotalPages;
                  result.CurrentPage = pagedListEntities.CurrentPage;
                  result.PageSize = pagedListEntities.PageSize;*/

        //return result;

        /*         return null;

             }

             public async Task DeleteCohortsUser(String userId, CancellationToken ct)
             {
                 try
                 {
                     var userCohortEntity = await _uow.UserCohorts.FirstAsync(c => c.UserId == userId, ct);
                     _uow.UserCohorts.Delete(userCohortEntity);
                     await _uow.SaveChangesAsync(ct);
                 }
                 catch (InvalidOperationException e)
                 {
                     _logger.LogError(e, string.Empty);
                 }
             }

             public async Task<bool> ExistsAsync(String userId, CancellationToken ct)
             {
                 if (await _uow.UserCohorts.FirstAsync(c => c.UserId == userId, ct) != null)
                     return true;

                 return false;
             }*/
    }
}
