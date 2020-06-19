using si2.dal.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDataflowRepository Dataflows { get; }
        IVehicleRepository Vehicles { get; }

        IDataflowVehicleRepository DataflowsVehicles { get; }

        IProgramLevelRepository ProgramLevels { get; }

        IContactInfoRepository ContactInfos { get; }
        IAddressRepository Addresses { get; }
        IInstitutionRepository Institutions { get; }
        IProgramRepository Programs { get; }
        ICohortRepository Cohorts { get; }
        IUserCohortRepository UserCohorts { get; }
        ICourseRepository Courses { get; }
        IUserCourseRepository UserCourses { get; }
        ICourseCohortRepository CourseCohorts { get; }

      
        IDocumentRepository Documents { get; }

        Task<int> SaveChangesAsync(CancellationToken ct);

        int SaveChanges();

        public void Dispose();
    }
}
