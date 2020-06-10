using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static si2.common.Enums;

namespace si2.dal.Entities
{
    [Table("ProgramLevel")]
    public class ProgramLevel : Si2BaseDataEntity<Guid>, IAuditable
    {
        /*
         * credits : float - mandatory
         * nameFr : string - mandatory - 30
         * nameAr : string - mandatory - 30
         * nameEn : string - mandatory - 30
         * universityId : Guid - mandatory
         */

        [Required]
        public float credits { get; set; }

        [Required]
        [StringLength(30)]
        public string nameFr { get; set; }

        [Required]
        [StringLength(30)]
        public string nameAr { get; set; }

        [Required]
        [StringLength(30)]
        public string nameEn { get; set; }

        [ForeignKey("Institution")]
        public Guid universityId { get; set; }

    }
}
