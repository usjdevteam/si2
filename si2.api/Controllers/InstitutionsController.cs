using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using si2.bll.Dtos.Requests.Institution;
using si2.bll.Dtos.Results.Institution;
using si2.bll.Helpers.ResourceParameters;
using si2.bll.Services;
using si2.common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/institutions")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public class InstitutionsController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<InstitutionsController> _logger;
        private readonly IInstitutionService _institutionService;

        public InstitutionsController(LinkGenerator linkGenerator, ILogger<InstitutionsController> logger, IInstitutionService institutionService)
        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _institutionService = institutionService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InstitutionDto))]
        public async Task<ActionResult> CreateInstitution([FromBody] CreateInstitutionDto createInstitutionDto, CancellationToken ct)
        {
            var institutionToReturn = await _institutionService.CreateInstitutionAsync(createInstitutionDto, ct);
            if (institutionToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetInstitution", new { id = institutionToReturn.Id }, institutionToReturn);
        }

        [HttpGet("{id}", Name = "GetInstitution")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InstitutionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetInstitution(Guid id, CancellationToken ct)
        {
            var institutionDto = await _institutionService.GetInstitutionByIdAsync(id, ct);

            if (institutionDto == null)
                return NotFound();

            return Ok(institutionDto);
        }

        [HttpGet(Name = "GetInstitutions")]
        public async Task<ActionResult> GetInstitutions([FromQuery]InstitutionResourceParameters pagedResourceParameters, CancellationToken ct)
        {
            var institutionDtos = await _institutionService.GetInstitutionsAsync(pagedResourceParameters, ct);

            var previousPageLink = institutionDtos.HasPrevious ? CreateInstitutionsResourceUri(pagedResourceParameters, Enums.ResourceUriType.PreviousPage) : null;
            var nextPageLink = institutionDtos.HasNext ? CreateInstitutionsResourceUri(pagedResourceParameters, Enums.ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = institutionDtos.TotalCount,
                pageSize = institutionDtos.PageSize,
                currentPage = institutionDtos.CurrentPage,
                totalPages = institutionDtos.TotalPages,
                previousPageLink,
                nextPageLink
            };

            if (institutionDtos == null)
                return NotFound();

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
            return Ok(institutionDtos);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InstitutionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateInstitution([FromRoute]Guid id, [FromBody] UpdateInstitutionDto updateInstitutionDto, CancellationToken ct)
        {
            if (!await _institutionService.ExistsAsync(id, ct))
                return NotFound();

            var institutionToReturn = await _institutionService.UpdateInstitutionAsync(id, updateInstitutionDto, ct);
            if (institutionToReturn == null)
                return BadRequest();

            return Ok(institutionToReturn);
        }

       
        private string CreateInstitutionsResourceUri(InstitutionResourceParameters pagedResourceParameters, Enums.ResourceUriType type)
        {
            switch (type)
            {
                case Enums.ResourceUriType.PreviousPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetInstitutions",
                        new
                        {
                            status = pagedResourceParameters.Status,
                            searchQuery = pagedResourceParameters.SearchQuery,
                            pageNumber = pagedResourceParameters.PageNumber - 1,
                            pageSize = pagedResourceParameters.PageSize
                        }); // TODO get the aboslute path 
                case Enums.ResourceUriType.NextPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetInstitutions",
                        new
                        {
                            status = pagedResourceParameters.Status,
                            searchQuery = pagedResourceParameters.SearchQuery,
                            pageNumber = pagedResourceParameters.PageNumber + 1,
                            pageSize = pagedResourceParameters.PageSize
                        });
                default:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetInstitutions",
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
