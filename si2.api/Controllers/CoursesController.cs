﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using si2.bll.Dtos.Requests.Course;
using si2.bll.Dtos.Requests.UserCourse;
using si2.bll.Dtos.Results.Course;
using si2.bll.Helpers.ResourceParameters;
using si2.bll.ResourceParameters;
using si2.bll.Services;
using si2.common;
using si2.dal.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CoursesController> _logger;
        private readonly ICourseService _CourseService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserCourseService _userCourseService;

        public CoursesController(LinkGenerator linkGenerator, ILogger<CoursesController> logger, UserManager<ApplicationUser> userManager, ICourseService CourseService, IUserCourseService userCourseService)
        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _CourseService = CourseService;
            _userManager = userManager;
            _userCourseService = userCourseService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
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
                        });

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


        [HttpGet("{id}/users", Name = "GetSubscribedUsers")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetSubscribedUsers([FromRoute]Guid id, [FromQuery]ApplicationUserResourceParameters pagedResourceParameters, CancellationToken ct)
        {
            var UserCourseDto = await _CourseService.GetUsersCourseAsync(id, pagedResourceParameters, ct);

            if (UserCourseDto == null)
                return NotFound();

            return Ok(UserCourseDto);
        }

         //subscribe users to a course
        [HttpPost("{id}/users", Name = "UpdateUsersCourse")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateUsersCourse([FromRoute] Guid id, [FromBody] ManageCoursesUserDto manageUsersToCourseDto, CancellationToken ct)
        {
            var course = await _CourseService.GetCourseEntityByIdAsync(id, ct);

            if (course == null)
            {
                return NotFound();
            }

            var userCourseToReturn = await _userCourseService.AssignUsersToCourseAsync(id, manageUsersToCourseDto, ct);
            return Ok();
        }
    }
}
