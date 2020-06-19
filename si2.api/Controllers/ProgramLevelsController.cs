using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
using si2.bll.Dtos.Requests.ProgramLevel;
using si2.bll.Dtos.Results.ProgramLevel;
using si2.bll.Services;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/programLevels")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public class ProgramLevelsController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<DataflowsController> _logger;
        private readonly IProgramLevelService _programLevelService;

        public ProgramLevelsController(LinkGenerator linkGenerator, ILogger<DataflowsController> logger, IProgramLevelService programLevelService)
        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _programLevelService = programLevelService;
        }

        [HttpGet("{id}", Name = "GetProgramLevel")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgramLevelDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetProgramLevel(Guid id, CancellationToken ct)
        {
            var ProgramLevelDto = await _programLevelService.GetProgramLevelByIdAsync(id, ct);

            if (ProgramLevelDto == null)
                return NotFound();

            return Ok(ProgramLevelDto);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProgramLevelDto))]
        public async Task<ActionResult> CreateProgramLevel([FromBody] CreateProgramLevelDto createProgramLevelDto, CancellationToken ct)
        {
            var ProgramLevelToReturn = await _programLevelService.CreateProgramLevelAsync(createProgramLevelDto, ct);
            if (ProgramLevelToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetProgramLevel", new { id = ProgramLevelToReturn.Id }, ProgramLevelToReturn);
        }


        [HttpGet(Name = "GetProgramLevels")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> GetProgramLevels(CancellationToken ct)
        {
            var dataflowDtos = await _programLevelService.GetProgramLevelsAsync(ct);

            if (dataflowDtos == null)
                return NotFound();




            return Ok(dataflowDtos);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgramLevelDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> UpdateDataflow([FromRoute]Guid id, [FromBody] UpdateProgramLevelDto UpdateProgramLevelDto, CancellationToken ct)
        {
            if (!await _programLevelService.ExistsAsync(id, ct))
                return NotFound();

            var dataflowToReturn = await _programLevelService.UpdateProgramLevelAsync(id, UpdateProgramLevelDto, ct);
            if (dataflowToReturn == null)
                return BadRequest();

            return Ok(dataflowToReturn);
        }
    }
}