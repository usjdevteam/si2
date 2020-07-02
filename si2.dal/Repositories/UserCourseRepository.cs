using si2.dal.Context;
using si2.dal.Entities;

namespace si2.dal.Repositories
{
    public class UserCourseRepository : Repository<UserCourse>, IUserCourseRepository
    {
        public UserCourseRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
