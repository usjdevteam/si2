using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.ContactInfo
{
    public class UpdateContactInfoDto
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
