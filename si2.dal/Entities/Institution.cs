using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static si2.common.Enums;

namespace si2.dal.Entities
{
    public class Institution : Si2BaseDataEntity<Guid>, IAuditable

    {
        [Required]
        public string name { get; set; }
    }
}
