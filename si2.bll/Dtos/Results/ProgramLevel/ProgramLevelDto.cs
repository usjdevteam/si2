using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static si2.common.Enums;

namespace si2.bll.Dtos.Results.ProgramLevel
{
    public class ProgramLevelDto
    {

        public Guid Id { get; set; }

        public float Credits { get; set; }

        public string NameFr { get; set; }

        public string NameAr { get; set; }

        public string NameEn { get; set; }

        public Guid UniversityId { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
