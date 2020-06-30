﻿using System;
using System.Collections.Generic;
using si2.dal.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.dal.Entities
{
    [Table("Address")]
    public class Address : Si2BaseDataEntity<Guid>, IAuditable
    {
        [Required]
        [MaxLength(100)]
        public string StreetFr { get; set; }

        [MaxLength(100)]
        public string StreetAr { get; set; }

        [Required]
        [MaxLength(50)]
        public string CityFr { get; set; }

        [MaxLength(50)]
        public string CityAr { get; set; }

        [Required]
        [MaxLength(50)]
        public string CountryFr { get; set; }

        [MaxLength(50)]
        public string CountryAr { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,7)")]
        public decimal Longitude { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,7)")]
        public decimal Latitude { get; set; }

        public ICollection<Institution> Institutions { get; set; }
    }
}
