using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace si2.dal.Entities
{
    [Table("ProgramLevel")]
    public class ProgramLevel : Si2BaseDataEntity<Guid>, IAuditable
    {
        public string Name { get; set; }
    }
}
