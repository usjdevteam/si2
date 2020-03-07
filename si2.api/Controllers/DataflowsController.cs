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
            if(dataflowToReturn == null)
                return BadRequest();

            return CreatedAtRoute("GetDataflow", new { id = dataflowToReturn.Id }, dataflowToReturn);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteDataflow(Guid id, CancellationToken ct)
        {
            await _dataflowService.DeleteDataflowByIdAsync(id, ct);
          
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


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DataflowDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateDataflow(Guid id, [FromBody] UpdateDataflowDto updateDataflowDto, CancellationToken ct)
        {
            if (updateDataflowDto == null)
                return BadRequest();

            var dataflowToReturn = await _dataflowService.UpdateDataflowAsync(id, updateDataflowDto, ct);
            if (dataflowToReturn == null)
                return BadRequest();

            return Ok(dataflowToReturn);
        }
    }
}
