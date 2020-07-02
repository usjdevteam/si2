using si2.dal.Context;
using si2.dal.Entities;


namespace si2.dal.Repositories
{
    public class ProgramLevelRepository : Repository<ProgramLevel>, IProgramLevelRepository
    {
        public ProgramLevelRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
