using Microsoft.AspNetCore.JsonPatch;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Requests.UserCourse;
using si2.bll.Dtos.Results.Course;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Dtos.Results.UserCohort;
using si2.bll.Dtos.Results.UserCourse;
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
    public interface IUserCourseService : IServiceBase
    {
        Task<UserCourseDto> AssignCoursesToUserAsync(String id, ManageCoursesUserDto addCoursesToUserDto, CancellationToken ct);
        Task<PagedList<CourseDto>> GetCoursesUserAsync(String userId, CancellationToken ct);
        Task<bool> ExistsAsync(String userId, CancellationToken ct);
        Task<UserCourseDto> AssignUsersToCourseAsync(Guid id, ManageCoursesUserDto addUsersToCourseDto, CancellationToken ct);
    }
}
