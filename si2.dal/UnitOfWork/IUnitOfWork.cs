using si2.dal.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDataflowRepository Dataflows { get; }
        IContactInfoRepository ContactInfos { get; }
        IAddressRepository Addresses { get; }

        IProgramRepository Programs { get; }

        IDocumentRepository Documents { get; }

        Task<int> SaveChangesAsync(CancellationToken ct);

        int SaveChanges();

        public void Dispose();
    }
}
