using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace si2.dal.Entities
{
    [Table("Address")]
    public class Address : Si2BaseEntity<Guid>
    {
        public string Location { get; set; }

        public virtual Institution InstitutionAttr { get; set; }
    }
}
