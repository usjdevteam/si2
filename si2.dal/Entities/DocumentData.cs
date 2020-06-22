using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace si2.dal.Entities
{
    [Table("DocumentData")]
    public class DocumentData : Si2BaseDataEntity<Guid>
    {
        [Required]
        public Guid DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public Document Document { get; set; }

        public byte[] FileBytes { get; set; }
    }
}