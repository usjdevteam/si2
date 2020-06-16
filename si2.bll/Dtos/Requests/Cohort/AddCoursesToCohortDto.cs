using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.Cohort
{
    public class AddCoursesToCohortDto
    {
        [Required]
        public List<string> CoursesIds { get; set; }
    }
}
