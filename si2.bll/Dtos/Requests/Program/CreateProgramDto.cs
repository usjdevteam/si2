using System;
using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Program
{
    public class CreateProgramDto
    {
        [Required]
        [Display(Name = "Code")]
        [StringLength(6, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Code { get; set; }

        [Required]
        [Display(Name = "NameFr")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameFr { get; set; }

        [Required]
        [Display(Name = "NameAr")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameAr { get; set; }

        [Required]
        [Display(Name = "NameEn")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameEn { get; set; }

        [Required]
        [Display(Name = "ProgramlevelId")]
        public Guid ProgramLevelId { get; set; } 

        [Required]
        [Display(Name = "InstitutionId")]
        public Guid InstitutionId { get; set; }
    }
}
