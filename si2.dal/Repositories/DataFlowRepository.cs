using Microsoft.EntityFrameworkCore;
using si2.dal.Context;
using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.Repositories
{
    public class DataflowRepository : Repository<Dataflow>, IDataflowRepository
    {
        public DataflowRepository(Si2DbContext _db) : base(_db)
        {
        }

        public async Task<Dataflow> GetCompleteDataflow(Guid dataflowId, CancellationToken ct)
        {
            var dataflow = await _db.Set<Dataflow>()
                .AsNoTracking()
                .Include(c => c.DataflowVehicles)
                .ThenInclude(c => c.Vehicle)
                .Where(c => c.Id == dataflowId)
                .FirstOrDefaultAsync(ct);

            return dataflow;
        }
    }
}
