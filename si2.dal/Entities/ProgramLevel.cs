using si2.dal.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace si2.dal.Entities
{
    [Table("ProgramLevel")]
    public class ProgramLevel : Si2BaseDataEntity<Guid>, IAuditable
    {
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public Decimal Credits { get; set; }

        [Required]
        [StringLength(30)]
        public string NameFr { get; set; }

        [Required]
        [StringLength(30)]
        public string NameAr { get; set; }

        [Required]
        [StringLength(30)]
        public string NameEn { get; set; }

        [ForeignKey("Institution")]
        public Guid InstitutionId { get; set; }
        public Institution Institution { get; set; }

    }
}
