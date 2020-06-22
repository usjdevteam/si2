using Microsoft.EntityFrameworkCore;
using si2.dal.Context;
using si2.dal.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.Repositories
{
   public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(Si2DbContext _db) : base(_db)
        {
        }

        public async Task<DocumentData> GetDocumentDataAsync(Guid id, CancellationToken ct)
        {
            return await _db.Set<DocumentData>().FirstOrDefaultAsync(c => c.DocumentId == id);
        }
    }
}
