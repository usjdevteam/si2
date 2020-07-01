using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.dal.Entities
{
    [Table("Cohort")]
    public class Cohort : Si2BaseDataEntity<Guid>, IAuditable
    {
      
        [Required]
        [StringLength(20, ErrorMessage = "Promotion field must be equal or below 20 characters")]
        public string Promotion { get; set; }

        [ForeignKey("ProgramId")]
        public Program Program { get; set; }
        public Guid ProgramId { get; set; }

        public ICollection<CourseCohort> CourseCohorts { get; set; }
        
        public ICollection<UserCohort> UserCohorts { get; set; }
    }
}
