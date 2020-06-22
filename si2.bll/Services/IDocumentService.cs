using si2.bll.Dtos.Requests.Document;
using si2.bll.Dtos.Results.Document;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IDocumentService : IServiceBase
    {
        Task<bool> UploadDocumentAsync(CreateDocumentDto createDocumentDto, byte[] fileData, string fileName, string contentType, string userID, CancellationToken ct);

        List<Document> GetDocuments();
       
        Task<DocumentDto> UpdateDocumentAsync(Guid id, UpdateDocumentDto updateDocumentDto, CancellationToken ct);

        Task<DocumentDto> SoftDeleteDocumentAsync(Guid id, SoftDeleteDocumentDto softDeleteDocumentDto, CancellationToken ct);
        
        Task<bool> ExistsAsync(Guid id, CancellationToken ct);
    }
}
