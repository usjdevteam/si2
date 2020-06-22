using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Document;
using si2.bll.Dtos.Results.Document;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public class DocumentService : ServiceBase, IDocumentService
    {
        public DocumentService(IUnitOfWork uow, IMapper mapper, ILogger<DocumentService> logger) : base(uow, mapper, logger)
        {
          
        }


       public async Task<bool> UploadDocumentAsync(CreateDocumentDto createDocumentDto, byte[] fileData, string fileName, string contentType, string userID, CancellationToken ct)
        {
            try
            {
                Document documentEntity = _mapper.Map<Document>(createDocumentDto);

                documentEntity.FileName = fileName;
                documentEntity.ContentType = contentType;
                documentEntity.FileData = fileData;
                documentEntity.UploadedBy = userID;
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


        public async Task<DocumentDto> SoftDeleteDocumentAsync(Guid id, SoftDeleteDocumentDto softDeleteDocumentDto, CancellationToken ct)
        {
            DocumentDto documentDto = null;

            try
            {
                var documentEntity = await _uow.Documents.GetAsync(id, ct);

                _mapper.Map(softDeleteDocumentDto, documentEntity);

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

      
        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        {
            if (await _uow.Documents.GetAsync(id, ct) != null)
                return true;

            return false;
        }
    }
}
