using si2.dal.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.dal.Entities
{
    [Table("Document")]
    public class Document : Si2BaseDataEntity<Guid>, IAuditable
    {
        [ForeignKey("UniversityId")]
        public Institution Institution { get; set; }
        public Guid? InstitutionId { get; set; }
     

        [ForeignKey("ProgramId")]
        public Program Program { get; set; }

        public Guid? ProgramId { get; set; }

    
        public string FileName { get; set; }

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
        public byte[] FileData { get; set; }

        [Required]
        public DateTime UploadedOn { get; set; }

        [Required]
        [ForeignKey("UploadedBy")]
        public virtual ApplicationUser User { get; set; }

        public string UploadedBy { get; set; }

        [Required]
        public bool IsDeleted { get; set; } 
    }
}
