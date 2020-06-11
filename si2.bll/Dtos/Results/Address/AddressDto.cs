using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.Dtos.Results.Address
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string StreetFr { get; set; }
        public string StreetAr { get; set; }

        public string CityFr { get; set; }

        public string CityAr { get; set; }

        public string CountryFr { get; set; }

        public string CountryAr { get; set; }
        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }
}
