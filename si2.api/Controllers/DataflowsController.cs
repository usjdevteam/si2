using EarnedCard.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Dtos.Results.Dataflow;
using si2.bll.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/dataflows")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public class DataflowsController : ControllerBase
    {
        private readonly ILogger<DataflowsController> _logger;
        private readonly IDataflowService _dataflowService;

        public DataflowsController(ILogger<DataflowsController> logger, IDataflowService dataflowService)
        {
            _logger = logger;
            _dataflowService = dataflowService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DataflowDto))]
        public async Task<ActionResult> CreateDataflow([FromBody] CreateDataflowDto createDataflowDto, CancellationToken ct)
        {
            if (createDataflowDto == null)
                return BadRequest();

            var dataflowToReturn = await _dataflowService.CreateDataflowAsync(createDataflowDto, ct);

            return CreatedAtRoute("GetDataflow", new { id = dataflowToReturn.Id }, dataflowToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> DeleteDataflow(Guid id, CancellationToken ct)
        {
            try
            {
                await _dataflowService.DeleteDataflowByIdAsync(id, ct);
            }
            catch (EntityNotFoundException e)
            {
                _logger.LogError(e, e.Message);
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{id}", Name = "GetDataflow")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataflowDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetDataflow(Guid id, CancellationToken ct)
        {
            var dataflowDto = await _dataflowService.GetDataflowByIdAsync(id, ct);

            if (dataflowDto == null)
                return NotFound();

            return Ok(dataflowDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetDataflows(CancellationToken ct)
        {
            var dataflowDtos = await _dataflowService.GetDataflowsAsync(ct);

            if (dataflowDtos == null)
                return NotFound();

            return Ok(dataflowDtos);
        }


        [HttpPut]
        public async Task<ActionResult> UpdateDataflow([FromBody] UpdateDataflowDto updateDataflowDto, CancellationToken ct)
        {
            var dataflowDtos = await _dataflowService.GetDataflowsAsync(ct);

            if (dataflowDtos == null)
                return NotFound();

            return Ok(dataflowDtos);
        }
    }
}
