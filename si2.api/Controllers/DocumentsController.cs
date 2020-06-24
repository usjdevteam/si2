using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using si2.bll.Dtos.Requests.Document;
using si2.bll.Dtos.Results.Document;
using si2.bll.ResourceParameters;
using si2.bll.Services;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using static si2.common.Enums;

namespace si2.api.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly ILogger<DocumentsController> _logger;
        private readonly IDocumentService _documentService;
        private readonly UserManager<ApplicationUser> _userManager;


        public DocumentsController(LinkGenerator linkGenerator, ILogger<DocumentsController> logger, IDocumentService documentService, UserManager<ApplicationUser> userManager)
        {
            _linkGenerator = linkGenerator;
            _logger = logger;
            _documentService = documentService;
            _userManager = userManager;
        }

       
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DocumentDto))]
        public async Task<IActionResult> UploadDocument(IFormFile file, [FromForm]string fileInfoText, CancellationToken ct)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(userEmail);

            byte[] fileBytesArray = null;
            
            using (var fileMemoryStream = new MemoryStream())
            {
                await file.CopyToAsync(fileMemoryStream, ct);
                fileBytesArray = fileMemoryStream.ToArray();
            }

            var documentToReturn = await _documentService.UploadDocumentAsync(
                JsonConvert.DeserializeObject<CreateDocumentDto>(fileInfoText),
                fileBytesArray,
                file.FileName,
                file.ContentType,
                user.Id,
                ct);

            return CreatedAtRoute("GetDocument", new { id = documentToReturn.Id }, documentToReturn);
        }



        [HttpGet("{id}", Name = "GetDocument")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetDocument([FromRoute] Guid id, CancellationToken ct)
        {
            if (!await _documentService.ExistsAsync(id, ct))
                return NotFound();

            var document = await _documentService.GetDocumentByIdAsync(id, ct);

            return Ok(document);
        }



        [HttpGet]
        [Route("download/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DownloadDocument([FromRoute] Guid id, CancellationToken ct)
        {
            if (await _documentService.ExistsAsync(id, ct) == false)
                return NotFound();


            var document = await _documentService.DownloadDocumentAsync(id, ct);


            if (document != null) 
            {
                return File(document.FileBytes, document.ContentType, document.OriginalFileName);
            }
            return NotFound();
        }


        [HttpGet(Name = "GetDocuments")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetDocuments([FromQuery]DocumentResourceParameters pagedResourceParameters, CancellationToken ct)
        {
            var documentDtos = await _documentService.GetDocumentsAsync(pagedResourceParameters, ct);

            var previousPageLink = documentDtos.HasPrevious ? CreateDocumentResourceUri(pagedResourceParameters, ResourceUriType.PreviousPage) : null;
            var nextPageLink = documentDtos.HasNext ? CreateDocumentResourceUri(pagedResourceParameters, ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = documentDtos.TotalCount,
                pageSize = documentDtos.PageSize,
                currentPage = documentDtos.CurrentPage,
                totalPages = documentDtos.TotalPages,
                previousPageLink,
                nextPageLink
            };

            if (documentDtos.Count < 1)
            {
                return NotFound();
            }

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
            return Ok(documentDtos);
        }


        private string CreateDocumentResourceUri(DocumentResourceParameters pagedResourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetDocuments",
                        new
                        {
                            pageNumber = pagedResourceParameters.PageNumber - 1,
                            pageSize = pagedResourceParameters.PageSize
                        });
                case ResourceUriType.NextPage:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetDocuments",
                        new
                        {
                            pageNumber = pagedResourceParameters.PageNumber + 1,
                            pageSize = pagedResourceParameters.PageSize
                        });
                default:
                    return _linkGenerator.GetUriByName(this.HttpContext, "GetDocuments",
                       new
                       {
                           pageNumber = pagedResourceParameters.PageNumber,
                           pageSize = pagedResourceParameters.PageSize
                       });
            }
        }



        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateDocument([FromRoute] Guid id, [FromBody] UpdateDocumentDto updateDocumentDto, CancellationToken ct)
        {
            if (!await _documentService.ExistsAsync(id, ct))
                return NotFound();


            DocumentDto documentToReturn = await _documentService.UpdateDocumentAsync(id, updateDocumentDto, ct);

            return Ok(documentToReturn);
        }


        [HttpPatch("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SoftDeleteDocument([FromRoute]Guid id, CancellationToken ct)
        {
            if (!await _documentService.ExistsAsync(id, ct))
                return NotFound();

            await _documentService.SoftDeleteDocumentAsync(id, ct);

            return NoContent();
        }
    }
}
