using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Course;
using si2.bll.Dtos.Results.Administration;
using si2.bll.Dtos.Results.Course;
using si2.bll.Helpers.PagedList;
using si2.bll.Helpers.ResourceParameters;
using si2.bll.ResourceParameters;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class CourseService : ServiceBase, ICourseService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseService(IUnitOfWork uow, IMapper mapper, ILogger<ICourseService> logger, UserManager<ApplicationUser> userManager) : base(uow, mapper, logger)
        {
            _userManager = userManager;
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto, CancellationToken ct)
        {
            CourseDto CourseDto = null;
            try
            {
                var CourseEntity = _mapper.Map<Course>(createCourseDto);
                await _uow.Courses.AddAsync(CourseEntity, ct);
                await _uow.SaveChangesAsync(ct);
                CourseDto = _mapper.Map<CourseDto>(CourseEntity);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, string.Empty);
            }
            return CourseDto;
        }


        public async Task<CourseDto> UpdateCourseAsync(Guid id, UpdateCourseDto updateCourseDto, CancellationToken ct)
        {
            CourseDto CourseDto = null;

            var updatedEntity = _mapper.Map<Course>(updateCourseDto);
            updatedEntity.Id = id;
            await _uow.Courses.UpdateAsync(updatedEntity, id, ct, updatedEntity.RowVersion);
            await _uow.SaveChangesAsync(ct);
            var CourseEntity = await _uow.Courses.GetAsync(id, ct);
            CourseDto = _mapper.Map<CourseDto>(CourseEntity);

            return CourseDto;
        }
        

        public async Task<CourseDto> GetCourseByIdAsync(Guid id, CancellationToken ct)
        {
            CourseDto CourseDto = null;

            var CourseEntity = await _uow.Courses.GetAsync(id, ct);
            if (CourseEntity != null)
            {
                CourseDto = _mapper.Map<CourseDto>(CourseEntity);
            }

            return CourseDto;
        }

        public async Task<Course> GetCourseEntityByIdAsync(Guid id, CancellationToken ct)
        {
            var CourseEntity = await _uow.Courses.GetAsync(id, ct);

            if (CourseEntity != null)
                return CourseEntity;
            else
                return null;
        }


        public async Task<PagedList<CourseDto>> GetCoursesAsync(DataflowResourceParameters resourceParameters, CancellationToken ct)
        {
            var CourseEntities = _uow.Courses.GetAll();

            if (!string.IsNullOrEmpty(resourceParameters.SearchQuery))
            {
                var searchQueryForWhereClause = resourceParameters.SearchQuery.Trim().ToLower();
                CourseEntities = CourseEntities
                    .Where(a => a.NameFr.ToLower().Contains(searchQueryForWhereClause)
                            || a.NameAr.ToLower().Contains(searchQueryForWhereClause)
                            || a.NameEn.ToLower().Contains(searchQueryForWhereClause));
            }

            var pagedListEntities = await PagedList<Course>.CreateAsync(CourseEntities,
                resourceParameters.PageNumber, resourceParameters.PageSize, ct);

            var result = _mapper.Map<PagedList<CourseDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;

            return result;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        {
            if (await _uow.Courses.GetAsync(id, ct) != null)
                return true;

            return false;
        }

        
        public async Task<PagedList<UserDto>> GetUsersCourseAsync(Guid courseId, ApplicationUserResourceParameters resourceParameters, CancellationToken ct)
        {
            var courseUsersIds = await _uow.UserCourses.FindByAsync(c => c.CourseId == courseId, ct);

            var usersIds = courseUsersIds.Select(c => c.UserId);

            var usersEntity = _userManager.Users.Where(user => usersIds.Contains(user.Id));

            var pagedListEntities = await PagedList<ApplicationUser>.CreateAsync(usersEntity, resourceParameters.PageNumber, resourceParameters.PageSize, ct);

            var result = _mapper.Map<PagedList<UserDto>>(pagedListEntities);
            result.TotalCount = pagedListEntities.TotalCount;
            result.TotalPages = pagedListEntities.TotalPages;
            result.CurrentPage = pagedListEntities.CurrentPage;
            result.PageSize = pagedListEntities.PageSize;

            return result;
        }
    }
}
