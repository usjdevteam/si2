using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Account
{
    public class ConfirmEmailRequestDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Token { get; set; }

    }
}
