using System;
using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Course
{
    public class CreateCourseDto
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string NameFr { get; set; }

        [Required]
        public string NameAr { get; set; }

        [Required]
        public string NameEn { get; set; }

        [Required]
        public decimal Credits { get; set; }

        [Required]
        public Guid InstitutionId { get; set; }
    }
}
