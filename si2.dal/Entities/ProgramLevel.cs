using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.dal.Entities
{
   public class ProgramLevel : Si2BaseDataEntity<Guid>, IAuditable
    {
        public string Name { get; set; }
    }
}
