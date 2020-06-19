using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Document
{
    public class SoftDeleteDocumentDto
    {
        [Required]
        [Display(Name = "isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
