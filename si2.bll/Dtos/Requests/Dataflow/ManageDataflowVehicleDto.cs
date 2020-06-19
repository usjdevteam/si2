using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.Dtos.Requests.Dataflow
{
    public class ManageDataflowVehicleDto
    {
        public List<Guid> AddedVehiclesIds { get; set; }
        public List<Guid> DeletedVehiclesIds { get; set; }
    }
}
