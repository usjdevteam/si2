using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using si2.bll.Dtos.Requests.Document;
using si2.bll.Dtos.Results.Document;
using si2.bll.Services;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<IActionResult> UploadDocument(IFormFile file, [FromForm] string fileInfoText, CancellationToken ct)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByEmailAsync(userEmail);

            byte[] fileBytesArray = null;
            
            using (var fileMemoryStream = new MemoryStream())
            {
                await file.CopyToAsync(fileMemoryStream, ct);
                fileBytesArray = fileMemoryStream.ToArray();
            }

            await _documentService.UploadDocumentAsync(

                JsonConvert.DeserializeObject<CreateDocumentDto>(fileInfoText),
                fileBytesArray,
                file.FileName,
                file.ContentType,
                user.Id,
                ct);

            return Ok();
        }


        [HttpGet("{id}", Name = "GetDocument")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetDocument(Guid id)
        {
            for (int i = 0; i < _documentService.GetDocuments().Count; i++)
            {
                if (id == _documentService.GetDocuments()[i].Id)
                {
                    
                    if(_documentService.GetDocuments()[i].IsDeleted.Equals(false))
                    {
                        string fileName = _documentService.GetDocuments()[i].FileName;

                        byte[] fileBytes = _documentService.GetDocuments()[i].FileData;

                        return File(fileBytes, "APPLICATION/octet-stream", fileName);
                    }
                }

            }

            return NotFound();
        }


        [HttpGet(Name = "GetDocuments")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetDocuments()
        {
            List<Document> docsReturned = new List<Document>();
            var docs = _documentService.GetDocuments();

            if(docs.Count() < 1)
            {
                return NoContent();
            }

            for (int i = 0; i < docs.Count; i++)
            {
                 if (docs[i].IsDeleted.Equals(false))
                 {
                   docsReturned.Add(docs[i]);  
                 }
            }

            if(docsReturned.Count() < 1)
            {
                return NoContent();
            }

            return Ok(docsReturned);
        }


        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DocumentDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateDocument([FromRoute]Guid id, [FromBody] UpdateDocumentDto updateDocumentDto, CancellationToken ct)
        {
            if (!await _documentService.ExistsAsync(id, ct))
                return NotFound();

            DocumentDto documentToReturn = null;
            var docs = _documentService.GetDocuments();

            for (int i = 0; i < docs.Count; i++)
            {
                if (id == docs[i].Id)
                {
                    if (docs[i].IsDeleted.Equals(false))
                    {
                        documentToReturn = await _documentService.UpdateDocumentAsync(id, updateDocumentDto, ct);
                    }
                }
            }

            if (documentToReturn == null) 
            { 
                return BadRequest();
            }

            return Ok(documentToReturn);
        }


        [HttpPatch("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SoftDeleteDocument([FromRoute]Guid id, [FromBody] SoftDeleteDocumentDto softDeleteDocumentDto, CancellationToken ct)
        {
            if (!await _documentService.ExistsAsync(id, ct))
                return NotFound();

            DocumentDto documentToReturn = null;
            var docs = _documentService.GetDocuments();

            for (int i = 0; i < docs.Count; i++)
            {
                if (id == docs[i].Id)
                {
                    if (docs[i].IsDeleted.Equals(false))
                    {
                        documentToReturn = await _documentService.SoftDeleteDocumentAsync(id, softDeleteDocumentDto, ct);
                    }
                }
            }

            if (documentToReturn == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
