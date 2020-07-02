using System;
using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Course
{
    public class CreateCourseDto
    {
        [Required]
        [Display(Name = "Code")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Code { get; set; }
       
        [Required]
        [Display(Name = "NameFr")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameFr { get; set; }
  
        [Required]
        [Display(Name = "NameAr")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameAr { get; set; }
        
        [Required]
        [Display(Name = "NameEn")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameEn { get; set; }

        [Required]
        [Display(Name = "Credits")]
        [RegularExpression(@"^[1-9]{1,3}(?:\.[0-9]{1,2})?$", ErrorMessage = "Please enter up to 5 decimal digits for Credits")]
        public decimal Credits { get; set; }

        [Required]
        [Display(Name = "InstitutionId")]
        public Guid InstitutionId { get; set; }
    }
}
