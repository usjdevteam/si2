
namespace si2.bll.Dtos.Results.ContactInfo
{
    public class ContactInfoDto
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public byte[] RowVersion { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
