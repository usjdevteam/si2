
using si2.bll.Dtos.Requests.Cohort;

using si2.bll.Dtos.Requests.CourseCohort;
using si2.bll.Dtos.Requests.UserCohort;

using si2.bll.Dtos.Requests.Dataflow;


using si2.bll.Dtos.Results.Administration;
using si2.bll.Dtos.Results.Cohort;
using si2.bll.Dtos.Results.Course;
using si2.bll.Helpers.PagedList;
using si2.bll.ResourceParameters;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface ICohortService : IServiceBase
    {
        Task<CohortDto> CreateCohortAsync(CreateCohortDto createCohortDto, CancellationToken ct);
        Task<CohortDto> GetCohortByIdAsync(Guid id, CancellationToken ct);
        Task<PagedList<CohortDto>> GetCohortsAsync(CancellationToken ct);



        Task AssignUsersToCohortAsync(Guid id, ManageCohortsUserDto manageCohortsUserDto, CancellationToken ct);


        Task UpdateUsersCohort(Guid id, AddUsersToCohortDto addUsersToCohortDto, CancellationToken ct);

        Task<PagedList<UserDto>> GetUsersCohortAsync(Guid cohortId, ApplicationUserResourceParameters resourceParameters, CancellationToken ct);

        Task AddCoursesToCohortAsync(Guid id, AddCoursesToCohortDto addCoursesToCohortDto, CancellationToken ct);

        Task<PagedList<CourseDto>> GetCoursesCohortAsync(Guid cohortId, ApplicationUserResourceParameters resourceParameters, CancellationToken ct);


        Task<bool> ExistsAsync(Guid id, CancellationToken ct);



    }
}
