using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.Institution
{
    public class UpdateInstitutionDto
    {
        [Required]
        public string NameFr { get; set; }

        [Required]
        public string NameAr { get; set; }

        [Required]
        public string NameEn { get; set; }

        [Required]
        public Guid AddressId { get; set; }

        [Required]
        public Guid ContactInfoId { get; set; }

        [Required]
        public byte[] RowVersion { get; set; }
    }
}
