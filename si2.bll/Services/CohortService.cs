using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Results.Administration;
using si2.bll.Dtos.Results.Cohort;
using si2.bll.Dtos.Results.Course;
using si2.bll.Helpers.PagedList;
using si2.bll.ResourceParameters;


using si2.dal.Entities;
using si2.dal.UnitOfWork;

using System;

using System.Linq;


using System.Threading;
using System.Threading.Tasks;
using si2.bll.Dtos.Requests.UserCohort;
using si2.bll.Dtos.Requests.CourseCohort;

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


            if (cohortEntities.Count() > 1)
            {
                var pagedListEntities = await PagedList<Cohort>.CreateAsync(cohortEntities, 1, cohortEntities.Count(), ct);

                var result = _mapper.Map<PagedList<CohortDto>>(pagedListEntities);
                result.TotalCount = pagedListEntities.TotalCount;
                result.TotalPages = pagedListEntities.TotalPages;
                result.CurrentPage = pagedListEntities.CurrentPage;
                result.PageSize = pagedListEntities.PageSize;

                return result;
            }
            return null;

        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        {
            if (await _uow.Cohorts.GetAsync(id, ct) != null)
                return true;

            return false;
        }

        /*public async Task AssignUsersToCohortAsync(Guid id, AddUsersToCohortDto addUsersToCohortDto, CancellationToken ct)
        {
            var usersCohort = _uow.UserCohorts.FindBy(c => c.CohortId == id).ToList();

            
            var usersCohortToAdd = addUsersToCohortDto.UsersIds.Where(u => !usersCohort.Any(uc => uc.UserId == u));
            
            foreach (var userId in usersCohortToAdd)
            {
                if(!usersCohort.Any(uc => uc.UserId == userId && uc.CohortId == id))
                {
                    _uow.UserCohorts.Add(new UserCohort() { CohortId = id, UserId = userId });
                }
                
            }
            
            await _uow.SaveChangesAsync(ct);
        }*/
        public async Task AssignUsersToCohortAsync(Guid cohortId, ManageCohortsUserDto manageCohortsUserDto, CancellationToken ct)
        {
            //var cohort = _uow.Cohorts.FindBy(c => c.Id == cohortId).First();
            var cohort = await _uow.Cohorts.GetAsync(cohortId, ct);



            if (manageCohortsUserDto.AddUsersIds != null)
            {
                foreach (var userId in manageCohortsUserDto.AddUsersIds)
                {
                    /*TO REFACTOR - to be same as the other functions
                     * TO SKIP ERRORS 
                     */
                    var usersCohort = _uow.UserCohorts.FindBy(c => c.CohortId == cohortId && c.UserId == userId).Count();

                    var isUser = _userManager.Users.Any(u => u.Id == userId);
                    if (usersCohort == 0 && isUser)
                    {
                        cohort.UserCohorts.Add(new UserCohort() { CohortId = cohortId, UserId = userId });
                    }

                }
            }

            if (manageCohortsUserDto.DeleteUsersIds != null)
            {
                foreach (var userId in manageCohortsUserDto.DeleteUsersIds)
                {

                    var userCohort = await _uow.UserCohorts.FirstAsync(c => c.CohortId == cohortId && c.UserId == userId, ct);

                    if (userCohort != null)
                    {
                        _uow.UserCohorts.Delete(userCohort);
                    }
                }
            }

            await _uow.SaveChangesAsync(ct);


        }


        public async Task<PagedList<UserDto>> GetUsersCohortAsync(Guid cohortId, ApplicationUserResourceParameters resourceParameters, CancellationToken ct)
        {
            var cohortUsersIds = await _uow.UserCohorts.FindByAsync(c => c.CohortId == cohortId, ct);

            var usersIds = cohortUsersIds.Select(c => c.UserId);

            var usersEntity = _userManager.Users.Where(user => usersIds.Contains(user.Id));


            var pagedListEntities = await PagedList<ApplicationUser>.CreateAsync(usersEntity, resourceParameters.PageNumber, resourceParameters.PageSize, ct);

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

        /*public async Task UpdateUsersCohort(Guid cohortId, AddUsersToCohortDto addUsersToCohortDto, CancellationToken ct)
        {

            // Get Item from database
            var usersCohort  = _uow.UserCohorts.FindBy(c => c.CohortId == cohortId).ToList();

            var usersCohortToDelete = usersCohort.Where(uc => !addUsersToCohortDto.UsersIds.Contains(uc.UserId)).ToList();

            var usersCohortToAdd = addUsersToCohortDto.UsersIds.Where(u=> !usersCohort.Any(uc => uc.UserId == u));
            

            foreach (var user in usersCohortToDelete)
            {
                var userCohortEntity = await _uow.UserCohorts.FirstAsync(uc => uc.CohortId == cohortId && uc.UserId == user.UserId, ct);
                _uow.UserCohorts.Delete(userCohortEntity);
            }

            foreach (var userid in usersCohortToAdd)
            {
                _uow.UserCohorts.Add(new UserCohort() { CohortId = cohortId, UserId = userid });
            }

            await _uow.SaveChangesAsync(ct);
            
        }*/


        /* -------------------------------------- COURSE COHORT -------------------------------------- */
        public async Task AddCoursesToCohortAsync(Guid cohortId, ManageCoursesCohortDto manageCoursesCohortDto, CancellationToken ct)
        {
            var cohort = await _uow.Cohorts.GetAsync(cohortId, ct);

            if (manageCoursesCohortDto.AddCoursesIds != null)
            {
                foreach (var courseId in manageCoursesCohortDto.AddCoursesIds)
                {

                    /*TO REFACTOR - to be same as the other functions
                    * TO SKIP ERRORS 
                    */
                    var courseCohort = _uow.CourseCohorts.FindBy(c => c.CohortId == cohortId && c.CourseId == courseId).Count();

                    if (courseCohort == 0)
                    {
                        cohort.CourseCohorts.Add(new CourseCohort() { CohortId = cohortId, CourseId = courseId });
                    }
                }
            }

            if (manageCoursesCohortDto.DeleteCoursesIds != null)
            {
                foreach (var courseId in manageCoursesCohortDto.DeleteCoursesIds)
                {
                    var courseCohort = await _uow.CourseCohorts.FirstAsync(c => c.CohortId == cohortId && c.CourseId == courseId, ct);

                    if (courseCohort != null)
                    {
                        _uow.CourseCohorts.Delete(courseCohort);
                    }
                }
            }

            await _uow.SaveChangesAsync(ct);

        }

        public async Task<PagedList<CourseDto>> GetCoursesCohortAsync(Guid cohortId, CourseResourceParameters resourceParameters, CancellationToken ct)
        {

            var CoursesEntity = _uow.CourseCohorts.GetAllIncluding(c => c.Course).Where(c => c.CohortId == cohortId).Select(c => c.Course);

            var pagedListEntities = await PagedList<Course>.CreateAsync(CoursesEntity, resourceParameters.PageNumber, resourceParameters.PageSize, ct);

            var result = _mapper.Map<PagedList<CourseDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;

            return result;

        }



        /*public async Task UpdateCourseCohortAsync(Guid cohortId, AddCoursesToCohortDto addCoursesToCohortDto, CancellationToken ct)
        {

            // Get Item from database
            var coursesCohort = _uow.CourseCohorts.FindBy(c => c.CohortId == cohortId).ToList();


            var coursesCohortToDelete = coursesCohort.Where(cc => !addCoursesToCohortDto.CoursesIds.Contains(cc.CourseId)).ToList();


            var coursesCohortToAdd = addCoursesToCohortDto.CoursesIds.Where(c => !coursesCohort.Any(cc => cc.CourseId == c));


            foreach (var course in coursesCohortToDelete)
            {
                var courseCohortEntity = await _uow.CourseCohorts.FirstAsync(uc => uc.CohortId == cohortId && uc.CourseId == course.CourseId, ct);
                _uow.CourseCohorts.Delete(courseCohortEntity);
            }

            foreach (var courseid in coursesCohortToAdd)
            {
                _uow.CourseCohorts.Add(new CourseCohort() { CohortId = cohortId, CourseId = courseid });
            }

            await _uow.SaveChangesAsync(ct);

        }*/


    }
}
