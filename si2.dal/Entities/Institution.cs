
using si2.dal.Interfaces;

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
        public string Code { get; set; }
        [Required]
        public string NameFr { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }


        public Guid AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        public Guid ContactInfoId { get; set; }
        [ForeignKey("ContactInfoId")]
        public virtual ContactInfo ContactInfo { get; set; }

        public Institution Parent { get; set; }
        public ICollection<Institution> Children { get; set; }

        public ICollection<Program> Programs { get; set; }

        public ICollection<Document> Documents { get; set; }
    }
}


