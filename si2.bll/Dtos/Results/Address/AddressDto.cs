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
        public byte[] RowVersion { get; set; }
        /*public override bool Equals(Object obj) => Equals(obj as AddressDto);

        public bool Equals(AddressDto obj)
        {
            return (this.Id == obj.Id
                && string.Equals(this.StreetFr, obj.StreetFr, StringComparison.OrdinalIgnoreCase)
                && string.Equals(this.StreetAr, obj.StreetAr, StringComparison.OrdinalIgnoreCase)
                && string.Equals(this.CityFr, obj.CityFr, StringComparison.OrdinalIgnoreCase)
                && string.Equals(this.CityAr, obj.CityAr, StringComparison.OrdinalIgnoreCase)
                && string.Equals(this.CountryFr, obj.CountryFr, StringComparison.OrdinalIgnoreCase)
                && string.Equals(this.CountryAr, obj.CountryAr, StringComparison.OrdinalIgnoreCase)
                && this.Longitude == obj.Longitude
                && this.Latitude == obj.Latitude);
        }*/

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
