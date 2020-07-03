using si2.dal.Context;
using si2.dal.Entities;

namespace si2.dal.Repositories
{
    public class UserCohortRepository : Repository<UserCohort>, IUserCohortRepository
    {
        public UserCohortRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
