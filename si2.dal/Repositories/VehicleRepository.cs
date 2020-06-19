using si2.dal.Context;
using si2.dal.Entities;

namespace si2.dal.Repositories
{
   public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
