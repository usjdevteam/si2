using System;
using System.ComponentModel.DataAnnotations.Schema;

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
