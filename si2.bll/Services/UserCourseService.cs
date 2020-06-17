using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Results.UserCourse;
using si2.bll.Dtos.Requests.UserCourse;
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
    public class UserCourseService : ServiceBase, IUserCourseService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCourseService(IUnitOfWork uow, IMapper mapper, ILogger<IUserCourseService> logger, UserManager<ApplicationUser> userManager) : base(uow, mapper, logger)
        {
            _userManager = userManager;
        }

        public async Task<UserCourseDto> AssignCoursesToUserAsync(String id, ManageCoursesUserDto manageUsersCoursesDto, CancellationToken ct)
        {
            UserCourseDto userCourseDto = null;

            var user = await _userManager.FindByIdAsync(id);

            if (manageUsersCoursesDto.AddCoursesIds != null)
            {
                foreach (var ac in manageUsersCoursesDto.AddCoursesIds)
                {
                    if (!user.UserCourses.Any(c => ac == c.CourseId))
                        user.UserCourses.Add(new UserCourse() { CourseId = ac, UserId = id });
                }
            }

            if (manageUsersCoursesDto.DeleteCoursesIds != null)
            {
                foreach (var dc in manageUsersCoursesDto.DeleteCoursesIds)
                {
                    var userCourse = user.UserCourses.FirstOrDefault(c => c.CourseId == dc && c.UserId == id);

                    if (userCourse != null)
                    {
                        user.UserCourses.Remove(userCourse);
                    }
                }
            }

            await _uow.SaveChangesAsync(ct);

            return userCourseDto;
        }

        public async Task<PagedList<UserCourseDto>> GetCoursesUserAsync(String userId, CancellationToken ct)
        {

            var coursesEntity = _userManager.Users
                .Include(u => u.UserCourses)
                .ThenInclude(uc => uc.Course)
                .Where(c => c.Id == userId).ToList();

            /*var pagedListEntities = await PagedList<ApplicationUser>.CreateAsync(cohortsEntity, 1, cohortsEntity.Count(), ct);

            var result = _mapper.Map<PagedList<UserCohortDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;*/

            //return result;

            return null;

        }

        public async Task<bool> ExistsAsync(String userId, CancellationToken ct)
        {
            if (await _uow.UserCourses.FirstAsync(c => c.UserId == userId, ct) != null)
                return true;

            return false;
        }
    }
}
