using si2.dal.Context;
using si2.dal.Entities;


namespace si2.dal.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}

