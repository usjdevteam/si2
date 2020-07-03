using si2.dal.Entities;
using System.Collections.Generic;

namespace si2.bll.Dtos.Results.UserCohort
{
    public class UserCohortDto
    {
        public List<ApplicationUser> Users { get; set; }
        public List<si2.dal.Entities.Cohort> Cohorts { get; set; }

    }
}
