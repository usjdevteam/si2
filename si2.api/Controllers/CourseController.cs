using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using si2.bll.Dtos.Requests.Course;
using si2.bll.Dtos.Results.Course;
using si2.bll.Helpers.ResourceParameters;
using si2.bll.Services;
using si2.common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public class CoursesController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CoursesController> _logger;
        private readonly ICourseService _CourseService;

        public CoursesController(LinkGenerator linkGenerator, ILogger<CoursesController> logger, ICourseService CourseService)
        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _CourseService = CourseService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CourseDto))]
        public async Task<ActionResult> CreateCourse([FromBody] CreateCourseDto createCourseDto, CancellationToken ct)
        {
            var CourseToReturn = await _CourseService.CreateCourseAsync(createCourseDto, ct);
            if (CourseToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetCourse", new { id = CourseToReturn.Id }, CourseToReturn);
        }

        [HttpGet("{id}", Name = "GetCourse")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCourse(Guid id, CancellationToken ct)
        {
            var CourseDto = await _CourseService.GetCourseByIdAsync(id, ct);

            if (CourseDto == null)
                return NotFound();

            return Ok(CourseDto);
        }

        [HttpGet(Name = "GetCourses")]
        public async Task<ActionResult> GetCourses([FromQuery]DataflowResourceParameters pagedResourceParameters, CancellationToken ct)
        {
            var CourseDtos = await _CourseService.GetCoursesAsync(pagedResourceParameters, ct);

            var previousPageLink = CourseDtos.HasPrevious ? CreateCoursesResourceUri(pagedResourceParameters, Enums.ResourceUriType.PreviousPage) : null;
            var nextPageLink = CourseDtos.HasNext ? CreateCoursesResourceUri(pagedResourceParameters, Enums.ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = CourseDtos.TotalCount,
                pageSize = CourseDtos.PageSize,
                currentPage = CourseDtos.CurrentPage,
                totalPages = CourseDtos.TotalPages,
                previousPageLink,
                nextPageLink
            };

            if (CourseDtos == null)
                return NotFound();

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
            return Ok(CourseDtos);
        }

        [HttpPost("{id}/Courses")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CourseDto))]
        public async Task<ActionResult> CreateChildCourse([FromRoute]Guid id, [FromBody] CreateCourseDto createCourseDto, CancellationToken ct)
        {
            if (!await _CourseService.ExistsAsync(id, ct))
                return NotFound();

            var CourseToReturn = await _CourseService.CreateChildCourseAsync(id, createCourseDto, ct);
            if (CourseToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetCourse", new { id = CourseToReturn.Id }, CourseToReturn);
        }

        [HttpGet("{id}/Courses", Name = "GetChildrenCourse")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetChildrenCourse(Guid id, CancellationToken ct)
        {
            var CourseDto = await _CourseService.GetChildrenCourseByIdAsync(id, ct);

            if (CourseDto == null)
                return NotFound();

            return Ok(CourseDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateCourse([FromRoute]Guid id, [FromBody] UpdateCourseDto updateCourseDto, CancellationToken ct)
        {
            if (!await _CourseService.ExistsAsync(id, ct))
                return NotFound();

            var CourseToReturn = await _CourseService.UpdateCourseAsync(id, updateCourseDto, ct);
            if (CourseToReturn == null)
                return BadRequest();

            return Ok(CourseToReturn);
        }

        private string CreateCoursesResourceUri(DataflowResourceParameters pagedResourceParameters, Enums.ResourceUriType type)
        {
            switch (type)
            {
                case Enums.ResourceUriType.PreviousPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetCourses",
                        new
                        {
                            status = pagedResourceParameters.Status,
                            searchQuery = pagedResourceParameters.SearchQuery,
                            pageNumber = pagedResourceParameters.PageNumber - 1,
                            pageSize = pagedResourceParameters.PageSize
                        }); // TODO get the aboslute path 
                case Enums.ResourceUriType.NextPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetCourses",
                        new
                        {
                            status = pagedResourceParameters.Status,
                            searchQuery = pagedResourceParameters.SearchQuery,
                            pageNumber = pagedResourceParameters.PageNumber + 1,
                            pageSize = pagedResourceParameters.PageSize
                        });
                default:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetCourses",
                       new
                       {
                           status = pagedResourceParameters.Status,
                           searchQuery = pagedResourceParameters.SearchQuery,
                           pageNumber = pagedResourceParameters.PageNumber,
                           pageSize = pagedResourceParameters.PageSize
                       });
            }
        }
    }
}
