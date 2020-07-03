
﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace si2.bll.Dtos.Requests.Address
{
    public class CreateAddressDto 
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
        [RegularExpression(@"^-?\d+\.\d{0,7}$", ErrorMessage = "Please enter up to 10 decimal digits for Longitude")]
        [Range(-180, 180)]
        [Column(TypeName = "decimal(10,7)")]
        public decimal Longitude { get; set; }

        [Required]
        [RegularExpression(@"^-?\d+\.\d{0,7}$", ErrorMessage = "Please enter up to 9 decimal digits for Latitude")]
        [Range(-90, 90)]
        [Column(TypeName = "decimal(9,7)")]
        public decimal Latitude { get; set; }
    }
}
