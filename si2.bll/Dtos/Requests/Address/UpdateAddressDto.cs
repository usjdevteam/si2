using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.bll.Dtos.Requests.Address
{
    public class UpdateAddressDto 
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string StreetFr { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string StreetAr { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string CityFr { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string CityAr { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string CountryFr { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string CountryAr { get; set; }

        [Required]
        [RegularExpression(@"^\d+\.\d{0,7}$")]
        [Range(0, 99.9999999)]
        [Column(TypeName = "decimal(9,7)")]
        public decimal Longitude { get; set; }

        [Required]
        [RegularExpression(@"^\d+\.\d{0,7}$")]
        [Range(0, 99.9999999)]
        [Column(TypeName = "decimal(9,7)")]
        public decimal Latitude { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
