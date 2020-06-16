using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using static si2.common.Enums;

namespace si2.bll.Dtos.Results.CourseCohortDto
{
    public class CourseCohortDto
    {
        public ICollection<si2.dal.Entities.Course> Courses { get; set; }

        public ICollection<si2.dal.Entities.Cohort> Cohorts { get; set; }
    }
}
