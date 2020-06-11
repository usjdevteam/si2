using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace si2.dal.Entities
{
    [Table("ContactInfo")]
    public class ContactInfo : Si2BaseEntity<Guid>
    {
        public string Name { get; set; }

        public virtual Institution InstitutionAttr { get; set; }
    }
}
