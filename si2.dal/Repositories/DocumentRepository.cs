using si2.dal.Context;
using si2.dal.Entities;

namespace si2.dal.Repositories
{
   public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
