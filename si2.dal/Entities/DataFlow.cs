using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.dal.Entities
{
    [Table("Dataflow")]
    public class Dataflow : Si2BaseDataEntity<Guid>, IAuditable
    {
        [Required]
        public string Title { get; set; }

        public ICollection<DataflowVehicle> DataflowVehicles { get; set; }
    }
}
