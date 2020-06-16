using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Administration
{
    public class UpdateUserDto
    {
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
