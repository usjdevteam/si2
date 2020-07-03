using si2.dal.Context;
using si2.dal.Entities;


namespace si2.dal.Repositories
{
    public class DataflowVehicleRepository : Repository<DataflowVehicle>, IDataflowVehicleRepository
    {
        public DataflowVehicleRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}

