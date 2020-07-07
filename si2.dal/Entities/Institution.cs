using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace si2.dal.Entities
{
    [Table("Institution")]
    public class Institution : Si2BaseDataEntity<Guid>, IAuditable
    {
        [Required]
        [MaxLength(6)]
        public string Code { get; set; }

        [Required]
        [MaxLength(400)]
        public string NameFr { get; set; }

        [Required]
        [MaxLength(400)]
        public string NameAr { get; set; }

        [Required]
        [MaxLength(400)]
        public string NameEn { get; set; }

        public Guid AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        [ForeignKey("ContactInfoId")]
        public Guid ContactInfoId { get; set; }
        public ContactInfo ContactInfo { get; set; }

        [ForeignKey("Parent")]
        public Guid? ParentId { get; set; }
        public Institution Parent { get; set; }

        public ICollection<Institution> Children { get; set; }

        public ICollection<Program> Programs { get; set; }

        public ICollection<Document> Documents { get; set; }
    }
}


