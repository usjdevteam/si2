using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.ProgramLevel
{
    public class CreateProgramLevelDto
    {
        [Required(ErrorMessage = "Credits field is mandatory")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Please enter valid float Number")]
        [Range(0, 999.99)]
        public float Credits { get; set; }

        [Required(ErrorMessage = "NameFr field is mandatory")]
        [StringLength(30, ErrorMessage = "NameFr field must be equal or below 30 characters")]
        public string NameFr { get; set; }

        [Required(ErrorMessage = "NameAr field is mandatory")]
        [StringLength(30, ErrorMessage = "NameAr field must be equal or below 30 characters")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "NameEn field is mandatory")]
        [StringLength(30, ErrorMessage = "NameEn field must be equal or below 30 characters")]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "UniversityId field is mandatory")]
        public Guid UniversityId { get; set; }
    }
}
