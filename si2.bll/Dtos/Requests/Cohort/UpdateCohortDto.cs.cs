using si2.bll.Dtos.Requests.UserCohort;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Cohort
{
    public class UpdateCohortDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Promotion field must be equal or below 20 characters")]
        public string Promotion { get; set; }

        [Required]
        public Guid ProgramId { get; set; }

        public List<ManageCohortsUserDto> UserCohorts { get; set; }

        [Required]
        public byte[] RowVersion { get; set; }

    }
}
