using si2.bll.Dtos.Requests.Address;
using si2.bll.Dtos.Requests.ContactInfo;
using System;
using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Institution
{
    public class UpdateInstitutionDto
    {
        [Required]
        [Display(Name = "Code")]
        [StringLength(6, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Code { get; set; }

        [Required]
        [Display(Name = "NameFr")]
        [StringLength(400, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameFr { get; set; }

        [Required]
        [Display(Name = "NameAr")]
        [StringLength(400, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameAr { get; set; }

        [Required]
        [Display(Name = "NameEn")]
        [StringLength(400, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameEn { get; set; }

        [Required]
        [Display(Name = "Address")]
        public UpdateAddressDto Address { get; set; }

        [Required]
        [Display(Name = "ContactInfo")]
        public UpdateContactInfoDto ContactInfo { get; set; }

        [Display(Name = "ParentId")]
        public Guid? ParentId { get; set; }

        [Required]
        public byte[] RowVersion { get; set; }
    }
}
