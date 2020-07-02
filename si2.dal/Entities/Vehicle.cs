using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.dal.Entities
{
    [Table("Vehicle")]
    public class Vehicle : Si2BaseDataEntity<Guid>
    {
        [Required]
        public string Name { get; set; }

        public ICollection<DataflowVehicle> DataflowVehicles { get; set; }

    }
}
