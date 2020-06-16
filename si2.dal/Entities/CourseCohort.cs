using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static si2.common.Enums;

namespace si2.dal.Entities
{
    [Table("CourseCohort")]
    public class CourseCohort : Si2BaseDataEntity<Guid>, IAuditable
    {
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public Guid CourseId { get; set; }
       

        [ForeignKey("CohortId")]
        public Cohort Cohort { get; set; }
        public Guid CohortId { get; set; }
    }
}
