using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Address
{
    public class UpdateAddressDto 
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
        public decimal Longitude { get; set; }

        [Required]
        public decimal Latitude { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
