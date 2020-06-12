using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        [Required]
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }
        public virtual Address Address { get; set; }


        [Required]
        [ForeignKey("ContactInfo")]
        public Guid ContactInfoId { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }

        public virtual Institution Parent { get; set; }
        public virtual ICollection<Institution> Children { get; set; }
    }
}