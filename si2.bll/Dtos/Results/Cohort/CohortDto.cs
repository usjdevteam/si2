using System;
using System.ComponentModel.DataAnnotations;

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

    }
}
