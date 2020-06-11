using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static si2.common.Enums;

namespace si2.dal.Entities
{
    [Table("Program")]
    public class Program : Si2BaseDataEntity<Guid>, IAuditable
    {
        [Required]
        [MaxLength(6)]
        public string Code { get; set; } 
        
        [Required]
        [MaxLength(100)]
        public string NameFr { get; set; }

        [Required]
        [MaxLength(100)]
        public string NameAr { get; set; }

        [Required]
        [MaxLength(100)]
        public string NameEn { get; set; }


        [Required]
        [ForeignKey("Programlevel")]
        public Guid ProgramlevelId { get; set; }

        [Required]
        [ForeignKey("Institution")]
        public Guid InstitutionId { get; set; }
    }
}
