using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using static si2.common.Enums;

namespace si2.bll.Dtos.Results.UserCohort
{
    public class UserCohortDto
    {
        public List<ApplicationUser> Users { get; set; }
        public List<si2.dal.Entities.Cohort> Cohorts { get; set; }

    }
}
