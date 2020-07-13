using System.ComponentModel.DataAnnotations;

namespace si2.tests.Dtos
{
    public class SendEmailDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "message")]
        public string Message { get; set; }
    }
}
