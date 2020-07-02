using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.dal.Entities
{
    [Table("Course")]
    public class Course : Si2BaseDataEntity<Guid>, IAuditable
    {
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(200)]
        public string NameFr { get; set; }

        [Required]
        [MaxLength(200)]
        public string NameAr { get; set; }

        [Required]
        [MaxLength(200)]
        public string NameEn { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Credits { get; set; }

        [ForeignKey("InstitutionId")]
        public Institution Institution { get; set; } 
        public Guid InstitutionId { get; set; }

        public ICollection<CourseCohort> CourseCohorts { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
    }
}
