using si2.dal.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.Repositories
{
    public interface IDataflowRepository : IRepository<Dataflow>
    {
        Task<Dataflow> GetCompleteDataflow(Guid dataflowId, CancellationToken ct);
    }
}
