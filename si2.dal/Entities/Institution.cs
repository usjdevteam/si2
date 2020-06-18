using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.dal.Entities
{
    [Table("Institution")]
    public class Institution : Si2BaseDataEntity<Guid>, IAuditable
    {       
        public string Name { get; set; }


        public ICollection<Program> Programs { get; set; }

        public ICollection<Document> Documents { get; set; }
    }
}
