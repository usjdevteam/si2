using System;
using System.Collections.Generic;

namespace si2.bll.Dtos.Requests.CourseCohort
{
    public class ManageCoursesCohortDto
    {
        public List<Guid> AddCoursesIds { get; set; }
        public List<Guid> DeleteCoursesIds { get; set; }

    }
}