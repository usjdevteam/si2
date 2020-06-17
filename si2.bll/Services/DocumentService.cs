using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Document;
using si2.bll.Dtos.Results.Document;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class DocumentService : ServiceBase, IDocumentService
    {
        //private readonly UserManager<ApplicationUser> _userManager;

        public DocumentService(IUnitOfWork uow, IMapper mapper, ILogger<DocumentService> logger/*, UserManager<ApplicationUser> userManager*/) : base(uow, mapper, logger)
        {
            //_userManager = userManager;
        }


        /*public async Task<bool> UploadDocumentAsync(CreateDocumentDto createDocumentDto, Guid universityId, Guid institutionId, Guid programId, string nameFr, string nameAr, string nameEn, string descriptionFr, string descriptionAr, string descriptionEn, string contentType, byte[] fileData, string uploadedBy, bool isDeleted, CancellationToken ct)
        {
           // var userId = _userManager.GetUserId(HttpContext.User);
           // var user = await _userManager.GetUserAsync(HttpContext.User);

            try
            {
                Document documentEntity = _mapper.Map<Document>(createDocumentDto);

                documentEntity.UniversityId = universityId;
                documentEntity.InstitutionId = institutionId;
                documentEntity.ProgramId = programId;

                documentEntity.NameFr = nameFr;
                documentEntity.NameAr = nameAr;
                documentEntity.NameEn = nameEn;

                documentEntity.DescriptionFr = descriptionFr;
                documentEntity.DescriptionAr = descriptionAr;
                documentEntity.DescriptionEn = descriptionEn;

                documentEntity.ContentType = contentType;
                documentEntity.FileData = fileData;
                documentEntity.UploadedOn = DateTime.UtcNow;
                documentEntity.UploadedBy = uploadedBy;
                documentEntity.IsDeleted = false;

                _uow.Documents.Add(documentEntity);  
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return await _uow.SaveChangesAsync(ct) > 0;
        }*/

       public async Task<bool> UploadDocumentAsync(CreateDocumentDto createDocumentDto, byte[] fileData, string fileName, string contentType, string userId, CancellationToken ct)
        {
            try
            {
                Console.WriteLine("Service:"+userId);
                Document documentEntity = _mapper.Map<Document>(createDocumentDto);
                documentEntity.FileName = fileName;
                documentEntity.ContentType = contentType;
                documentEntity.FileData = fileData;
                documentEntity.UploadedBy = userId;
                //documentEntity.UploadedBy = "ja";
                //documentEntity.UploadedOn = DateTime.UtcNow;
                documentEntity.UploadedOn = DateTime.Now;

                _uow.Documents.Add(documentEntity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return await _uow.SaveChangesAsync(ct) > 0;
        }


        public List<Document> GetDocuments()
        {
            List<Document> result = new List<Document>();

            try
            {
                result = _uow.Documents.GetAll().ToList();
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return result;
        }

        public async Task<DocumentDto> UpdateDocumentAsync(Guid id, UpdateDocumentDto updateDocumentDto, CancellationToken ct)
        {
            DocumentDto documentDto = null;

            try
            {
                var documentEntity = await _uow.Documents.GetAsync(id, ct);

                _mapper.Map(updateDocumentDto, documentEntity);

                await _uow.Documents.UpdateAsync(documentEntity, id, ct, documentEntity.RowVersion);
                await _uow.SaveChangesAsync(ct);

                documentEntity = await _uow.Documents.GetAsync(id, ct);
                documentDto = _mapper.Map<DocumentDto>(documentEntity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return documentDto;
        }

        public async Task<DocumentDto> PartialUpdateDocumentAsync(Guid id, UpdateDocumentDto updateDocumentDto, CancellationToken ct)
        {
            DocumentDto documentDto = null;

            try
            {
                var documentEntity = await _uow.Documents.GetAsync(id, ct);

                _mapper.Map(updateDocumentDto, documentEntity);

                await _uow.Documents.UpdateAsync(documentEntity, id, ct, documentEntity.RowVersion);
                await _uow.SaveChangesAsync(ct);

                documentEntity = await _uow.Documents.GetAsync(id, ct);
                documentDto = _mapper.Map<DocumentDto>(documentEntity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return documentDto;
        }


        public async Task<UpdateDocumentDto> GetUpdateDocumentDto(Guid id, CancellationToken ct) 
        {
            UpdateDocumentDto updateDocumentDto = null;

            try
            {
                var documentEntity = await _uow.Documents.GetAsync(id, ct);
                updateDocumentDto = _mapper.Map<UpdateDocumentDto>(documentEntity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            return updateDocumentDto;
        }


        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        {
            if (await _uow.Documents.GetAsync(id, ct) != null)
                return true;

            return false;
        }
    }
}
