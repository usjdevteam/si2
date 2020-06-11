using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace si2.dal.Entities
{
    [Table("Institution")]

    public class Institution : Si2BaseDataEntity<Guid>, IAuditable
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }

}
    

