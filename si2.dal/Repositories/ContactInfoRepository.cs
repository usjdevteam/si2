using si2.dal.Context;
using si2.dal.Entities;

namespace si2.dal.Repositories
{
    public class ContactInfoRepository : Repository<ContactInfo>, IContactInfoRepository
    {
        public ContactInfoRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
