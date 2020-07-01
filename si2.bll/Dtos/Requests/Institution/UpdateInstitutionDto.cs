using si2.bll.Dtos.Requests.Address;
using si2.bll.Dtos.Requests.ContactInfo;
using System;
using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Institution
{
    public class UpdateInstitutionDto
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string NameFr { get; set; }

        [Required]
        public string NameAr { get; set; }

        [Required]
        public string NameEn { get; set; }

        [Required]
        public UpdateAddressDto Address { get; set; }

        [Required]
        public UpdateContactInfoDto ContactInfo { get; set; }

        public Guid? ParentId { get; set; }

        [Required]
        public byte[] RowVersion { get; set; }
    }
}
