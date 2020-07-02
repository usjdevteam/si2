using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Account
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstNameFr { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastNameFr { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstNameAr { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastNameAr { get; set; }
    }
}
