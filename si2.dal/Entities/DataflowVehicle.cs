using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace si2.dal.Entities
{
    [Table("DataflowVehicle")]
    public class DataflowVehicle
    {
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
        public Guid VehicleId { get; set; }


        [ForeignKey("DataflowId")]
        public Dataflow Dataflow { get; set; }
        public Guid DataflowId { get; set; }
    }
}
