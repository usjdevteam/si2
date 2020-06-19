using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Results.Cohort;
using si2.bll.ResourceParameters;
using si2.bll.Services;
using si2.common;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/cohorts")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public class CohortsController : ControllerBase

    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CohortsController> _logger;
        private readonly ICohortService _cohortService;


        public CohortsController(LinkGenerator linkGenerator, ILogger<CohortsController> logger, ICohortService cohortService)

        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _cohortService = cohortService;

        }

        /* APIs
         * -----------------------------------
         * POST /api/cohorts
         * GET /api/cohorts
         * POST /api/cohorts/{id}/users
         * PUT /api/cohorts/{id}/users
         * GET /api/cohorts/{id}/users
         * POST /api/cohorts/{id}/courses
         */


         /*-------------------------------- COHORT -------------------------------- */

        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Bearer")]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CohortDto))]
        public async Task<ActionResult> CreateStudent([FromBody] CreateCohortDto createCohortDto, CancellationToken ct)
        {
            var cohortToReturn = await _cohortService.CreateCohortAsync(createCohortDto, ct);
            if (cohortToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetCohort", new { id = cohortToReturn.Id }, cohortToReturn);
        }

        [HttpGet("{id}", Name = "GetCohort")]

        //[Authorize(AuthenticationSchemes = "Bearer")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CohortDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCohort(Guid id, CancellationToken ct)
        {
            var cohortDto = await _cohortService.GetCohortByIdAsync(id, ct);

            if (cohortDto == null)
                return NotFound();

            return Ok(cohortDto);
        }

        [HttpGet(Name = "GetCohorts")]

        //[Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<ActionResult> GetCohorts(CancellationToken ct)
        {
            var cohortDtos = await _cohortService.GetCohortsAsync(ct);

            if (cohortDtos == null)
                return NotFound();

            return Ok(cohortDtos);
        }

        /*-------------------------------- USERS COHORT -------------------------------- */
        [HttpPost]
        [Route("{id}/users", Name = "AddUsersToCohort")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddUsersToCohort([FromRoute]Guid id, [FromBody] AddUsersToCohortDto addUsersToCohortDto, CancellationToken ct)

        {
            if (!await _cohortService.ExistsAsync(id, ct))
                return NotFound();


            await _cohortService.AssignUsersToCohortAsync(id, addUsersToCohortDto, ct);

            return Ok();


        }

        [HttpGet]
        [Route("{id}/users", Name = "GetUsersSubscribedToCohort")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> GetUsersSubscribedToCohort([FromRoute]Guid id, [FromQuery]ApplicationUserResourceParameters pagedResourceParameters, CancellationToken ct)
        {

            var userDtos = await _cohortService.GetUsersCohortAsync(id, pagedResourceParameters, ct);

            var previousPageLink = userDtos.HasPrevious ? CreateUserResourceUri(pagedResourceParameters, Enums.ResourceUriType.PreviousPage) : null;
            var nextPageLink = userDtos.HasNext ? CreateUserResourceUri(pagedResourceParameters, Enums.ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = userDtos.TotalCount,
                pageSize = userDtos.PageSize,
                currentPage = userDtos.CurrentPage,
                totalPages = userDtos.TotalPages,
                previousPageLink,
                nextPageLink
            };

            if (userDtos.Count < 1)
                return NotFound();

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

            return Ok(userDtos);


        }

        [HttpPut]
        [Route("{id}/users", Name = "GetUsersSubscribedToCohort")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> UpdateUsersCohort([FromRoute]Guid id, [FromBody] AddUsersToCohortDto addUsersToCohortDto, CancellationToken ct)
        {
            if (!await _cohortService.ExistsAsync(id, ct))
                return NotFound();

            await _cohortService.UpdateUsersCohort(id, addUsersToCohortDto, ct);

            return Ok();
        }

        /*-------------------------------- COURSE COHORT -------------------------------- */
        [HttpPost]
        [Route("{id}/courses", Name = "AddCoursesToCohort")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddCoursesToCohort([FromRoute]Guid id, [FromBody] AddCoursesToCohortDto addCoursesToCohortDto, CancellationToken ct)
        {
            if (!await _cohortService.ExistsAsync(id, ct))
                return NotFound();

            await _cohortService.AddCoursesToCohortAsync(id, addCoursesToCohortDto, ct);

            return Ok();


        }

        private string CreateUserResourceUri(ApplicationUserResourceParameters pagedResourceParameters, Enums.ResourceUriType type)
        {
            switch (type)
            {
                case Enums.ResourceUriType.PreviousPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetUsersSubscribedToCohort",
                        new
                        {
                            pageNumber = pagedResourceParameters.PageNumber - 1,
                            pageSize = pagedResourceParameters.PageSize
                        });
                case Enums.ResourceUriType.NextPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetUsersSubscribedToCohort",
                        new
                        {
                            pageNumber = pagedResourceParameters.PageNumber + 1,
                            pageSize = pagedResourceParameters.PageSize
                        });
                default:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetUsersSubscribedToCohort",
                       new
                       {
                           pageNumber = pagedResourceParameters.PageNumber,
                           pageSize = pagedResourceParameters.PageSize
                       });
            }
        }

    }
}
