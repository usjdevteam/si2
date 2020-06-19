using Microsoft.Extensions.DependencyInjection;
using si2.dal.Context;
using si2.dal.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace si2.dal.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly Si2DbContext _db;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(Si2DbContext db, IServiceProvider serviceProvider)
        {
            _db = db;
            _serviceProvider = serviceProvider;
        }

        public IDataflowRepository Dataflows => _serviceProvider.GetService<IDataflowRepository>();
        public IProgramLevelRepository ProgramLevels => _serviceProvider.GetService<IProgramLevelRepository>();

        public IVehicleRepository Vehicles => _serviceProvider.GetService<IVehicleRepository>();
        public IDataflowVehicleRepository DataflowsVehicles => _serviceProvider.GetService<IDataflowVehicleRepository>();


        public IProgramLevelRepository ProgramLevels => _serviceProvider.GetService<IProgramLevelRepository>();

        public IContactInfoRepository ContactInfos => _serviceProvider.GetService<IContactInfoRepository>();
        public IAddressRepository Addresses => _serviceProvider.GetService<IAddressRepository>();
        public IInstitutionRepository Institutions => _serviceProvider.GetService<IInstitutionRepository>();
        public IProgramRepository Programs => _serviceProvider.GetService<IProgramRepository>();
        public ICohortRepository Cohorts => _serviceProvider.GetService<ICohortRepository>();
        public IUserCohortRepository UserCohorts => _serviceProvider.GetService<IUserCohortRepository>();
        public ICourseRepository Courses => _serviceProvider.GetService<ICourseRepository>();
        public IUserCourseRepository UserCourses => _serviceProvider.GetService<IUserCourseRepository>();
        public ICourseCohortRepository CourseCohorts => _serviceProvider.GetService<ICourseCohortRepository>();

        public IDocumentRepository Documents => _serviceProvider.GetService<IDocumentRepository>();

        public async Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return await _db.SaveChangesAsync(ct);
        }
         
        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
