using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using si2.bll.Dtos.Requests.Program;
using si2.bll.Dtos.Results.Program;
using si2.bll.Helpers.ResourceParameters;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProgramDto))]
        public async Task<ActionResult> CreateProgram([FromBody] CreateProgramDto createProgramDto, CancellationToken ct)
        {
            var programToReturn = await _programService.CreateProgramAsync(createProgramDto, ct);
            if (programToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetProgram", new { id = programToReturn.Id }, programToReturn);
        }


        [HttpGet(Name = "GetPrograms")]
        public async Task<ActionResult> GetPrograms(CancellationToken ct)
        {
            var programDtos = await _programService.GetProgramAsync(ct);

            if (programDtos == null)
                return NotFound();

            return Ok(programDtos);
        }
    }
}
