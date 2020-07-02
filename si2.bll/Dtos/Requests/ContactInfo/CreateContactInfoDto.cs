using System.ComponentModel.DataAnnotations;


namespace si2.bll.Dtos.Requests.ContactInfo
{
    public class CreateContactInfoDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }

        [MaxLength(30)]
        public string Fax { get; set; }
    }
}
