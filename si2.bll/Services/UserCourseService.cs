using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.UserCourse;
using si2.bll.Dtos.Results.Course;
using si2.bll.Dtos.Results.UserCourse;
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
    public class UserCourseService : ServiceBase, IUserCourseService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICourseService _CourseService;

        public UserCourseService(IUnitOfWork uow, IMapper mapper, ILogger<IUserCourseService> logger, UserManager<ApplicationUser> userManager, ICourseService CourseService) : base(uow, mapper, logger)
        {
            _userManager = userManager;
            _CourseService = CourseService;
        }
        public async Task<UserCourseDto> AssignCoursesToUserAsync(String id, ManageCoursesUserDto manageUsersCoursesDto, CancellationToken ct)
        {
            UserCourseDto userCourseDto = null;

            var user = await _userManager.FindByIdAsync(id);

            if (manageUsersCoursesDto.AddCoursesIds != null)
            {
                if(user.UserCourses == null)
                    user.UserCourses = new List<UserCourse>();

                foreach (var ac in manageUsersCoursesDto.AddCoursesIds)
                {
                    var usersCourse = _uow.UserCourses.FindBy(c => c.CourseId == ac && c.UserId == id).Count();

                    if (usersCourse == 0)
                    {
                        user.UserCourses.Add(new UserCourse() { CourseId = ac, UserId = id });
                    }
                }
            }

            if (manageUsersCoursesDto.DeleteCoursesIds != null)
            {
                foreach (var dc in manageUsersCoursesDto.DeleteCoursesIds)
                {
                    var userCourse = _uow.UserCourses.FindBy(c => c.CourseId == dc && c.UserId == id).FirstOrDefault();

                    if (userCourse != null)
                    {
                        _uow.UserCourses.Delete(userCourse);
                    }
                }
            }

            await _uow.SaveChangesAsync(ct);

            return userCourseDto;
        }

        public async Task<PagedList<CourseDto>> GetCoursesUserAsync(String userId, CancellationToken ct)
        {

            var coursesEntity = _uow.UserCourses.GetAllIncluding(c => c.Course)
                                .Where(c => c.UserId == userId)
                                .Select(c => c.Course);

            if (coursesEntity.Count() > 0)
            {
                var pagedListEntities = await PagedList<Course>.CreateAsync(coursesEntity, 1, coursesEntity.Count(), ct);

                var result = _mapper.Map<PagedList<CourseDto>>(pagedListEntities);
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
            if (await _uow.UserCourses.FirstAsync(c => c.UserId == userId, ct) != null)
                return true;

            return false;
        }

        public async Task<UserCourseDto> AssignUsersToCourseAsync(Guid id, ManageCoursesUserDto manageUsersCoursesDto, CancellationToken ct)
        {
            UserCourseDto userCourseDto = null;

            var course = await _CourseService.GetCourseEntityByIdAsync(id, ct);

            if (manageUsersCoursesDto.AddUsersIds != null)
            {
                if (course.UserCourses == null)
                    course.UserCourses = new List<UserCourse>();

                foreach (var ac in manageUsersCoursesDto.AddUsersIds)
                {
                    var usersCourse = _uow.UserCourses.FindBy(c => c.UserId == ac && c.CourseId == id).Count();

                    if (usersCourse == 0)
                    {
                        course.UserCourses.Add(new UserCourse() { UserId = ac, CourseId = id });
                    }
                }
            }

            if (manageUsersCoursesDto.DeleteUsersIds != null)
            {
                foreach (var dc in manageUsersCoursesDto.DeleteUsersIds)
                {
                    var userCourse = _uow.UserCourses.FindBy(c => c.UserId == dc && c.CourseId == id).FirstOrDefault();

                    if (userCourse != null)
                    {
                        _uow.UserCourses.Delete(userCourse);
                    }
                }
            }

            await _uow.SaveChangesAsync(ct);

            return userCourseDto;
        }
    }
}