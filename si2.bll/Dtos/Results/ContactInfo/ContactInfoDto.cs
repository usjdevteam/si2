using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.Dtos.Results.ContactInfo
{
    public class ContactInfoDto
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public byte[] RowVersion { get; set; }
        public override bool Equals(Object obj) => Equals(obj as ContactInfoDto);

        public bool Equals(ContactInfoDto obj)
        {
            return (string.Equals(this.Email, obj.Email, StringComparison.OrdinalIgnoreCase)
                && string.Equals(this.Phone, obj.Phone, StringComparison.OrdinalIgnoreCase)
                && string.Equals(this.Fax, obj.Fax, StringComparison.OrdinalIgnoreCase));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
