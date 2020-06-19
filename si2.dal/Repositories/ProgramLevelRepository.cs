using si2.dal.Context;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace si2.dal.Repositories
{
    public class ProgramLevelRepository : Repository<ProgramLevel>, IProgramLevelRepository
    {
        public ProgramLevelRepository(Si2DbContext _db) : base(_db)
        {
        }
    }
}
