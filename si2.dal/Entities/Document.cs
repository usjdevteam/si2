using si2.dal.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.dal.Entities
{
    [Table("Document")]
    public class Document : Si2BaseDataEntity<Guid>, IAuditable
    {

        [ForeignKey("InstitutionId")]
        public Guid? InstitutionId { get; set; }

        public Institution Institution { get; set; }


        [ForeignKey("ProgramId")]
        public Guid? ProgramId { get; set; }

        public Program Program { get; set; }


        public string OriginalFileName { get; set; }

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
        [MaxLength(100)]
        public string DescriptionFr { get; set; }

        [Required]
        [MaxLength(100)]
        public string DescriptionAr { get; set; }

        [Required]
        [MaxLength(100)]
        public string DescriptionEn { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public DocumentData DocumentData { get; set; }

        [Required]
        public DateTime UploadedOn { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UploadedBy { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
