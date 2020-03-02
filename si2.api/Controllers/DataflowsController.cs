using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos;
using si2.bll.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> CreateDataflow([FromBody] CreatDataflowDto createDataflowDto, CancellationToken ct)
        {
            if (createDataflowDto == null)
                return BadRequest();

            await _dataflowService.CreateCourseAsync(createDataflowDto, ct);

            //if (courseToReturn == null)
            //    throw new Exception("Creating a course failed on save.");

            return null;
            // return CreatedAtRoute("GetCourse", new { id = courseToReturn.Id }, courseToReturn);
        }
    }
}
