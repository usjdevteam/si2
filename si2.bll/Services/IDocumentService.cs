using si2.bll.Dtos.Requests.Document;
using si2.bll.Dtos.Results.Document;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IDocumentService : IServiceBase
    {
        /*Task<bool> UploadDocumentAsync(CreateDocumentDto createDocumentDto,  
                                Guid universityId,
                                Guid institutionId,
                                Guid programId,
                                string nameFr,
                                string nameAr,
                                string nameEn,
                                string descriptionFr,
                                string descriptionAr,
                                string descriptionEn,
                                string contentType,
                                byte[] fileData,
                                string uploadedBy,
                                bool isDeleted,
                                CancellationToken ct);*/

        Task<bool> UploadDocumentAsync(CreateDocumentDto createDocumentDto, byte[] fileData, string fileName, string contentType, string userId, CancellationToken ct);

        List<Document> GetDocuments();

        Task<DocumentDto> UpdateDocumentAsync(Guid id, UpdateDocumentDto updateDocumentDto, CancellationToken ct);

        Task<DocumentDto> PartialUpdateDocumentAsync(Guid id, UpdateDocumentDto patchDoc, CancellationToken ct);

        Task<UpdateDocumentDto> GetUpdateDocumentDto(Guid id, CancellationToken ct);

        Task<bool> ExistsAsync(Guid id, CancellationToken ct);
    }
}
