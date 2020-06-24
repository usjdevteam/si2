using si2.bll.Dtos.Requests.Document;
using si2.bll.Dtos.Results.Document;
using si2.bll.Helpers.PagedList;
using si2.bll.ResourceParameters;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IDocumentService : IServiceBase
    {
        Task<DocumentDto> UploadDocumentAsync(CreateDocumentDto createDocumentDto, byte[] fileData, string fileName, string contentType, string userID, CancellationToken ct);

        Task<DocumentDto> GetDocumentByIdAsync(Guid id, CancellationToken ct);


        Task<DownloadDocumentDto> DownloadDocumentAsync(Guid id, CancellationToken ct);

  
        Task<PagedList<DocumentDto>> GetDocumentsAsync(DocumentResourceParameters pagedResourceParameters, CancellationToken ct);


        Task<DocumentDto> UpdateDocumentAsync(Guid id, UpdateDocumentDto updateDocumentDto, CancellationToken ct);

        Task SoftDeleteDocumentAsync(Guid id, CancellationToken ct);


        Task<bool> ExistsAsync(Guid id, CancellationToken ct);


        Task<bool> IsDeletedAsync(Guid id, CancellationToken ct);   
    }
}
