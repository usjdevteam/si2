using System.Collections.Generic;

namespace si2.bll.Dtos.Results.CourseCohortDto
{
    public class CourseCohortDto
    {
        public ICollection<si2.dal.Entities.Course> Courses { get; set; }

        public ICollection<si2.dal.Entities.Cohort> Cohorts { get; set; }
    }
}
