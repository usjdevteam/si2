using si2.dal.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.dal.Entities
{
    [Table("CourseCohort")]
    public class CourseCohort : Si2BaseDataEntity<Guid>, IAuditable
    {
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public Guid CourseId { get; set; }
       
        [ForeignKey("CohortId")]
        public Cohort Cohort { get; set; }
        public Guid CohortId { get; set; }
    }
}
