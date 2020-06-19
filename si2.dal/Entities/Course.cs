using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static si2.common.Enums;

namespace si2.dal.Entities
{
    [Table("Course")]
    public class Course : Si2BaseDataEntity<Guid>, IAuditable
    {
       
        [Required]

        [StringLength(10, ErrorMessage = "Code field must be equal or below 10 characters")]
        public string Code { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Name field must be equal or below 200 characters")]
        public string NameFr { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Name field must be equal or below 200 characters")]
        public string NameAr { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Name field must be equal or below 200 characters")]
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
