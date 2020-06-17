using si2.dal.Context;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.dal.Repositories
{
   public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
