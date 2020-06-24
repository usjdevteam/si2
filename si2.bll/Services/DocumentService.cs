using AutoMapper;
using Microsoft.Extensions.Logging;
using si2.bll.Dtos.Requests.Document;
using si2.bll.Dtos.Results.Document;
using si2.bll.Helpers.PagedList;
using si2.bll.ResourceParameters;
using si2.dal.Entities;
using si2.dal.UnitOfWork;
using System;
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


       public async Task<DocumentDto> UploadDocumentAsync(CreateDocumentDto createDocumentDto, byte[] fileData, string fileName, string contentType, string userID, CancellationToken ct)
        {
            Document documentEntity = null;

            try
            {
                documentEntity = _mapper.Map<Document>(createDocumentDto);

                documentEntity.OriginalFileName = fileName;
                documentEntity.ContentType = contentType;
                documentEntity.DocumentData = new DocumentData() { FileBytes = fileData, Document = documentEntity };
                documentEntity.UploadedBy = userID;
                documentEntity.UploadedOn = DateTime.Now;

                _uow.Documents.Add(documentEntity);
                await _uow.SaveChangesAsync(ct);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }

            documentEntity = await _uow.Documents.GetAsync(documentEntity.Id, ct);
            var documentDto = _mapper.Map<DocumentDto>(documentEntity);

            return documentDto;
        }


        public async Task<DocumentDto> GetDocumentByIdAsync(Guid id, CancellationToken ct)
        {
            var document = await _uow.Documents.GetAsync(id, ct);

            if (document == null)
                return null;

            var result = _mapper.Map<DocumentDto>(document);

            return result;
        }


        public async Task<DownloadDocumentDto> DownloadDocumentAsync(Guid id, CancellationToken ct)
        {
            var documentData = await _uow.Documents.GetDocumentDataAsync(id, ct);
            var document = await _uow.Documents.GetAsync(id, ct);

            if (document == null)
                return null;

            var result = new DownloadDocumentDto()
            {
                ContentType = document.ContentType,
                FileBytes = documentData.FileBytes,
                OriginalFileName = document.OriginalFileName
            };

            return result;
        }



        public async Task<PagedList<DocumentDto>> GetDocumentsAsync(DocumentResourceParameters resourceParameters, CancellationToken ct)
        {
            PagedList<DocumentDto> result = new PagedList<DocumentDto>();

            try
            {
                var documentEntities = _uow.Documents.GetAll().Where(doc => doc.IsDeleted == false);
                var instiutionIdFromLink = resourceParameters.InstitutionId;
                var programIdFromLink = resourceParameters.ProgramId;


                if (instiutionIdFromLink != null && instiutionIdFromLink != Guid.Empty)
                {
                    documentEntities = documentEntities
                        .Where(doc => doc.InstitutionId.Equals(instiutionIdFromLink));
                }

                if (programIdFromLink != null && programIdFromLink != Guid.Empty)
                {
                    documentEntities = documentEntities
                        .Where(doc => doc.ProgramId.Equals(programIdFromLink));
                }


                var pagedListEntities = await PagedList<Document>.CreateAsync(documentEntities,
                resourceParameters.PageNumber, resourceParameters.PageSize, ct);

                result = _mapper.Map<PagedList<DocumentDto>>(pagedListEntities);
                result.TotalCount = pagedListEntities.TotalCount;
                result.TotalPages = pagedListEntities.TotalPages;
                result.CurrentPage = pagedListEntities.CurrentPage;
                result.PageSize = pagedListEntities.PageSize;
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


        public async Task SoftDeleteDocumentAsync(Guid id, CancellationToken ct)
        {
            try
            {
                var documentEntity = await _uow.Documents.GetAsync(id, ct);
                documentEntity.IsDeleted = true;
                //await _uow.Documents.UpdateAsync(documentEntity, id, ct, documentEntity.RowVersion);
                await _uow.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
            }
        }


        public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        {
            if (await _uow.Documents.GetAsync(id, ct) != null)
                return true;

            return false;
        }


        public async Task<bool> IsDeletedAsync(Guid id, CancellationToken ct)
        {
            var document = await _uow.Documents.GetAsync(id, ct);
            if (document == null || document.IsDeleted == true)
                return true;

            return false;
        }
    }
}
