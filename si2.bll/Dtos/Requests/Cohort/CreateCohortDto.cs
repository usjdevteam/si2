﻿using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.Cohort
{
    public class CreateCohortDto
    {

        [Required]
        [StringLength(20, ErrorMessage = "Promotion field must be equal or below 20 characters")]
        public string Promotion { get; set; }

        [Required]
        public Guid ProgramId { get; set; }

        //public ICollection<CourseCohort> CourseCohorts { get; set; }

        public ICollection<UserCohort> UserCohorts { get; set; }
    }
}
