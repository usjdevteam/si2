using System;

namespace si2.bll.Dtos.Results.Course
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string NameFr { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public float Credits { get; set; }
        public Guid InstitutionId { get; set; }
        public byte[] RowVersion { get; set; }

        public override bool Equals(Object obj) => Equals(obj as CourseDto);

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

