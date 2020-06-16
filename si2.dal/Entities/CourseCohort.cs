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
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }

        [ForeignKey("Cohort")]
        public Guid CohortId { get; set; }
        public virtual Cohort Cohort { get; set; }

    }
}
