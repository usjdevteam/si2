using System;
using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Course
{
    public class UpdateCourseDto
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
        public float Credits { get; set; }

        [Required]
        public Guid InstitutionId { get; set; }

        [Required]
        public byte[] RowVersion { get; set; }

    }
}
