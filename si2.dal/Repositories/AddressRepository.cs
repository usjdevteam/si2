using si2.dal.Entities;
using si2.dal.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.dal.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
