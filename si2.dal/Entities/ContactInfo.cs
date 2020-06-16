using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace si2.dal.Entities
{
    [Table("ContactInfo")]
    public class ContactInfo : Si2BaseDataEntity<Guid>, IAuditable
    {
        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }

        [MaxLength(30)]
        public string Fax { get; set; }

        public ICollection<Institution> Institutions { get; set; }
    }
}
