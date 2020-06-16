using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using static si2.common.Enums;

namespace si2.bll.Dtos.Results.Cohort
{
    public class CohortDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Promotion field must be equal or below 20 characters")]
        public string Promotion { get; set; }

        [Required]
        public Guid ProgramId { get; set; }

       // public ICollection<CourseCohort> CourseCohorts { get; set; }

       // public ICollection<UserCohort> UserCohorts { get; set; }
    }
}
