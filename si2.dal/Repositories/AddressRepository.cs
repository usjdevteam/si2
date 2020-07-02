using si2.dal.Entities;
using si2.dal.Context;

namespace si2.dal.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
