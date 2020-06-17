using si2.dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
