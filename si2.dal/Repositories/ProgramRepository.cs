using si2.dal.Context;
using si2.dal.Entities;

namespace si2.dal.Repositories
{
    public class ProgramRepository : Repository<Program>, IProgramRepository
    {
        public ProgramRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
