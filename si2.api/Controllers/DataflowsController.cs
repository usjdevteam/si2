using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Dataflow;
using si2.bll.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/dataflows")]
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
            await _dataflowService.DeleteDataflowByIdAsync(id, ct);

            return NoContent();
        }

        [HttpGet("{id}", Name = "GetDataflow")]
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
    }
}
