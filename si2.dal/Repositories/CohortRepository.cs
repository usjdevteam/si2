using si2.dal.Context;
using si2.dal.Entities;

namespace si2.dal.Repositories
{
    public class CohortRepository : Repository<Cohort>, ICohortRepository
    {
        public CohortRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
