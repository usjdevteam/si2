using si2.bll.Dtos.Results.Address;
using si2.bll.Dtos.Results.ContactInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static si2.common.Enums;

namespace si2.bll.Dtos.Results.Institution
{
    public class InstitutionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string NameFr { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public AddressDto Address { get; set; }
        public ContactInfoDto ContactInfo { get; set; }
        public byte[] RowVersion { get; set; }

        public override bool Equals(Object obj) => Equals(obj as InstitutionDto);

        public bool Equals(InstitutionDto obj)
        {
             if (obj == null)
                return false;
            else if (this == obj)
                return true;
            else
                return (this.Id == obj.Id
                    && string.Equals(this.Code, obj.Code, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(this.NameFr, obj.NameFr, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(this.NameAr, obj.NameAr, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(this.NameEn, obj.NameEn, StringComparison.OrdinalIgnoreCase)
                    && this.Address == obj.Address
                    && this.ContactInfo == obj.ContactInfo
                    && this.RowVersion.SequenceEqual(obj.RowVersion));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}