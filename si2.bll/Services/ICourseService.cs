using Microsoft.AspNetCore.JsonPatch;
using si2.bll.Dtos.Requests.Course;
using si2.bll.Dtos.Results.Administration;
using si2.bll.Dtos.Results.Course;
using si2.bll.Dtos.Results.UserCourse;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using si2.bll.ResourceParameters;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface ICourseService : IServiceBase
    {
        Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto, CancellationToken ct);
        Task<CourseDto> CreateChildCourseAsync(Guid id, CreateCourseDto createCourseDto, CancellationToken ct);
        Task<CourseDto> UpdateCourseAsync(Guid id, UpdateCourseDto updateCourseDto, CancellationToken ct);
        Task<CourseDto> GetCourseByIdAsync(Guid id, CancellationToken ct);
        Task<CourseDto> GetChildrenCourseByIdAsync(Guid id, CancellationToken ct);
        Task<PagedList<CourseDto>> GetCoursesAsync(DataflowResourceParameters pagedResourceParameters, CancellationToken ct);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct);
        Task<PagedList<UserDto>> GetUsersCourseAsync(Guid id, ApplicationUserResourceParameters resourceParameters, CancellationToken ct);
        Task<Course> GetCourseEntityByIdAsync(Guid id, CancellationToken ct);
    }
}
