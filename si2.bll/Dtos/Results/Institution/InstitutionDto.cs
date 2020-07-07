using si2.bll.Dtos.Results.Address;
using si2.bll.Dtos.Results.ContactInfo;
using System;

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
        public InstitutionDto Parent { get; set; }
        //public Guid? ParentId { get; set; }
        public byte[] RowVersion { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}