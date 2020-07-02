using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using si2.dal.Entities;
using si2.dal.UnitOfWork;

namespace si2.bll.Services
{
    public class CourseCohortService : ServiceBase, ICourseCohortService
    {
        //the implementation is done in the cohort object

        private readonly UserManager<ApplicationUser> _userManager;

        public CourseCohortService(IUnitOfWork uow, IMapper mapper, ILogger<IUserCourseService> logger, UserManager<ApplicationUser> userManager) : base(uow, mapper, logger)
        {
            _userManager = userManager;
        }

    }
}
