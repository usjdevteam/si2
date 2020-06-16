using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace si2.bll.Dtos.Requests.ProgramLevel
{
    public class UpdateProgramLevelDto
    {
        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 999.99)]
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

        [Required]
        public Guid InstitutionId { get; set; }

        [Required]
        public byte[] RowVersion { get; set; }
    }

}
