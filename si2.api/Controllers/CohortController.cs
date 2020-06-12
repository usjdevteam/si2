using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using si2.bll.Dtos.Requests.Cohort;
using si2.bll.Dtos.Results.Cohort;
using si2.bll.Helpers.ResourceParameters;
using si2.bll.Services;
using si2.common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/cohorts")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CohortController :  ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<CohortController> _logger;
        private readonly ICohortService _cohortService;
        private readonly IUserCohortService _userCohortService;

        public CohortController(LinkGenerator linkGenerator, ILogger<CohortController> logger, ICohortService cohortService, IUserCohortService userCohortService)
        //public CohortController(LinkGenerator linkGenerator, ILogger<CohortController> logger, ICohortService cohortService)
        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _cohortService = cohortService;
            _userCohortService = userCohortService; 
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

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> GetCohorts(CancellationToken ct)
        {
            var cohortDtos = await _cohortService.GetCohortsAsync(ct);

            if (cohortDtos == null)
                return NotFound();

            return Ok(cohortDtos);
        }


      /*  [HttpPost("{id}")]
        [Route("{id}/users", Name = "AddUsersToCohort")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddUsersToCohort([FromRoute]Guid id, [FromBody] JObject users, CancellationToken ct)
        {
            if (!await _cohortService.ExistsAsync(id, ct))
                return NotFound();

            var userArray = users.Children<JProperty>().FirstOrDefault(x => x.Name == "userIds").Value;


            foreach (var userIdToAdd in userArray)
            {
                var userCohortToReturn = await _userCohortService.AssignUsersToCohortAsync(id, new Guid(userIdToAdd.ToString()), ct);
                if (userCohortToReturn == null)
                    return BadRequest();
            }

            return Ok();


        }
        */

        [HttpGet("{id}")]
        [Route("{id}/users", Name = "GetUsersSubscribedToCohort")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> GetUsersSubscribedToCohort([FromRoute]Guid cohortId, CancellationToken ct)
        {

            var UsercohortDtos = await _userCohortService.GetUsersCohortAsync(cohortId,ct);

            if (UsercohortDtos == null)
                return NotFound();

            return Ok(UsercohortDtos);


        }

    }
}
