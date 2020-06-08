﻿using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static si2.common.Enums;

namespace si2.dal.Entities
{
    [Table("Institution")]
    public class Institution : Si2BaseDataEntity<Guid>, IAuditable
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string NameFr { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }

        [Required]
        public Guid AddressId { get; set; }
        [Required]
        public Guid ContactInfoId { get; set; }
    }
}
