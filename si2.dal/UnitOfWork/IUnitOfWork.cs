using si2.dal.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDataflowRepository Dataflows { get; }
        IContactInfoRepository ContactInfos { get; }
        IAddressRepository Addresses { get; }
        IInstitutionRepository Institutions { get; }
        IProgramRepository Programs { get; }

        Task<int> SaveChangesAsync(CancellationToken ct);

        int SaveChanges();

        public void Dispose();
    }
}
