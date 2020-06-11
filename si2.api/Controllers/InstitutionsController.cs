using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using si2.bll.Services;
using si2.bll.Dtos.Requests.Institution;
using si2.bll.Dtos.Results.Institution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using si2.dal.Entities;
using si2.common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;

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
        private readonly IMapper _mapper;

        public InstitutionsController(LinkGenerator linkGenerator, ILogger<InstitutionsController> logger, IInstitutionService institutionService)
        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _institutionService = institutionService;
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

            //return Ok("Reached");
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InstitutionDto))]
        public async Task<ActionResult> CreateInstitutionForUniversity(Guid universityId,[FromBody] CreateInstitutionDto createInstitutionDto, CancellationToken ct)
        {
            //var institutionEntity = _mapper.Map<Institution>(createInstitutionDto);
            //var institutionToReturn = await _institutionService.CreateInstitutionAsync(universityId,institutionEntity, ct);

            var institutionToReturn = await _institutionService.CreateInstitutionAsync(createInstitutionDto, ct);
            if (institutionToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetInstitution", new { universityId = universityId, id = institutionToReturn.Id }, institutionToReturn);
        }
    }
}
