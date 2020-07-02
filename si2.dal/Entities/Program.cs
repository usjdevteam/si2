using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("ProgramLevelId")]
        public ProgramLevel ProgramLevel { get; set; }
        public Guid ProgramLevelId { get; set; }

        [ForeignKey("InstitutionId")]
        public Institution Institution { get; set; }
        public Guid InstitutionId { get; set; }

        public ICollection<Document> Documents { get; set; }

    }
}
