using si2.bll.Dtos.Requests.Address;
using si2.bll.Dtos.Requests.ContactInfo;
using System;
using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Institution
{
    public class CreateInstitutionDto
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
        public CreateAddressDto Address { get; set; }

        [Required]
        public CreateContactInfoDto ContactInfo { get; set; }

        public Guid? ParentId { get; set; }
    }
}
