using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using si2.bll.Dtos.Requests.Program; 
using si2.bll.Dtos.Results.Program;
using si2.bll.ResourceParameters;
using si2.bll.Services;
using si2.common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/programs")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ProgramsController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<ProgramsController> _logger;
        private readonly IProgramService _programService;

        public ProgramsController(LinkGenerator linkGenerator, ILogger<ProgramsController> logger, IProgramService programService)
        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _programService = programService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProgramDto))]
        public async Task<ActionResult> CreateProgram([FromBody] CreateProgramDto createProgramDto, CancellationToken ct)
        {
            var programToReturn = await _programService.CreateProgramAsync(createProgramDto, ct);
            if (programToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetProgram", new { id = programToReturn.Id }, programToReturn);
        }


        [HttpGet("{id}", Name = "GetProgram")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgramDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetProgram(Guid id, CancellationToken ct)
        { 
            var programDto = await _programService.GetProgramByIdAsync(id, ct);

            if (programDto == null)
                return NotFound();

            return Ok(programDto);
        }

        [HttpGet(Name = "GetPrograms")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgramDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetPrograms([FromQuery]ProgramResourceParameters pagedResourceParameters, CancellationToken ct)
        {
            var programDtos = await _programService.GetProgramAsync(pagedResourceParameters, ct);

            var previousPageLink = programDtos.HasPrevious ? CreateProgramResourceUri(pagedResourceParameters, Enums.ResourceUriType.PreviousPage) : null;
            var nextPageLink = programDtos.HasNext ? CreateProgramResourceUri(pagedResourceParameters, Enums.ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = programDtos.TotalCount,
                pageSize = programDtos.PageSize,
                currentPage = programDtos.CurrentPage,
                totalPages = programDtos.TotalPages,
                previousPageLink,
                nextPageLink
            };

            if (programDtos.Count < 1)
            {
                return NoContent();
            }


            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
            return Ok(programDtos);
        }

        private string CreateProgramResourceUri(ProgramResourceParameters pagedResourceParameters, Enums.ResourceUriType type)
        {
            switch (type)
            {
                case Enums.ResourceUriType.PreviousPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetPrograms",
                        new
                        {
                            pageNumber = pagedResourceParameters.PageNumber - 1,
                            pageSize = pagedResourceParameters.PageSize
                        });
                case Enums.ResourceUriType.NextPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetPrograms",
                        new
                        {
                            pageNumber = pagedResourceParameters.PageNumber + 1,
                            pageSize = pagedResourceParameters.PageSize
                        });
                default:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetPrograms",
                       new
                       {
                           pageNumber = pagedResourceParameters.PageNumber,
                           pageSize = pagedResourceParameters.PageSize
                       });
            }
        }
    }
}
