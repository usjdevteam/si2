using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Requests.UserCourse;
using si2.bll.Dtos.Results.Cohort;
using si2.bll.Dtos.Results.Course;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Dtos.Results.UserCohort;
using si2.bll.Dtos.Results.UserCourse;
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
                    //var userCourse = user.UserCourses.FirstOrDefault(c => c.CourseId == dc && c.UserId == id);
                    var userCourse = user.UserCourses.Where(c => c.CourseId == dc && c.UserId == id).FirstOrDefault();

                    if (userCourse != null)
                    {
                        user.UserCourses.Remove(userCourse);
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

            var pagedListEntities = await PagedList<Course>.CreateAsync(coursesEntity, 1, coursesEntity.Count(), ct);

            var result = _mapper.Map<PagedList<CourseDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;

            return result;


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
                foreach (var ac in manageUsersCoursesDto.AddUsersIds)
                {
                    if (!course.UserCourses.Any(c => ac == c.UserId))
                        course.UserCourses.Add(new UserCourse() { UserId = ac, CourseId = id });
                }
            }

            if (manageUsersCoursesDto.DeleteUsersIds != null)
            {
                foreach (var dc in manageUsersCoursesDto.DeleteUsersIds)
                {
                    var userCourse = course.UserCourses.Where(c => c.UserId == dc && c.CourseId == id).FirstOrDefault();

                    if (userCourse != null)
                    {
                        course.UserCourses.Remove(userCourse);
                    }
                }
            }

            await _uow.SaveChangesAsync(ct);

            return userCourseDto;
        }

        public async Task DeleteUsersCourse(Guid id, ManageCoursesUserDto manageUsersCoursesDto, CancellationToken ct)
        {
            try
            {
                if (manageUsersCoursesDto.DeleteUsersIds != null)
                {
                    foreach (var dc in manageUsersCoursesDto.DeleteUsersIds)
                    {
                        var userCourse = await _uow.UserCourses.FirstAsync(c => c.UserId == dc && c.CourseId == id, ct);

                        if (userCourse != null)
                        {
                            _uow.UserCourses.Delete(userCourse);
                        }
                    }
                    await _uow.SaveChangesAsync(ct);
                }
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e, string.Empty);
            }
        }
        
    }
}