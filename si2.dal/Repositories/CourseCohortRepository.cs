using si2.dal.Context;
using si2.dal.Entities;

namespace si2.dal.Repositories
{
    public class CourseCohortRepository : Repository<CourseCohort>, ICourseCohortRepository
    {
        public CourseCohortRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
