using System;

namespace si2.bll.Dtos.Results.Document
{
    public class DocumentDto
    {
        //public Guid UniversityId { get; set; }

        public Guid InstitutionId { get; set; }

        public Guid ProgramId { get; set; }

        public string NameFr { get; set; }

        public string NameAr { get; set; }

        public string NameEn { get; set; }

        public string DescriptionFr { get; set; }

        public string DescriptionAr { get; set; }

        public string DescriptionEn { get; set; }

        public string ContentType { get; set; }

        public byte[] FileData { get; set; }

        public DateTime UploadedOn { get; set; }

        public string UploadedBy { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
