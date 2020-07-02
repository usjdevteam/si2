using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Account
{
    public class ForgotPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
