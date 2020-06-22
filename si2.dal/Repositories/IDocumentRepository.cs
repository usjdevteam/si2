using si2.dal.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.Repositories
{
    public interface IDocumentRepository : IRepository<Document>
    {
        Task<DocumentData> GetDocumentDataAsync(Guid id, CancellationToken ct);
    }
}
