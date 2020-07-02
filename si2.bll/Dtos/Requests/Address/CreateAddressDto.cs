using System;
using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Address
{
    public class CreateAddressDto 
    {
        [Required]
        [Display(Name = "StreetFr")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string StreetFr { get; set; }

        [Display(Name = "StreetAr")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string StreetAr { get; set; }

        [Required]
        [Display(Name = "CityFr")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string CityFr { get; set; }

        [Display(Name = "CityAr")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string CityAr { get; set; }

        [Required]
        [Display(Name = "CountryFr")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string CountryFr { get; set; }

        [Display(Name = "CountryAr")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string CountryAr { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        [RegularExpression(@"^-?\d+\.\d{0,7}$", ErrorMessage = "Please enter up to 10 decimal digits for Longitude")]
        [Range(-180, 180)]
        public decimal Longitude { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        [RegularExpression(@"^-?\d+\.\d{0,7}$", ErrorMessage = "Please enter up to 9 decimal digits for Latitude")]
        [Range(-90, 90)]
        public decimal Latitude { get; set; }
    }
}
