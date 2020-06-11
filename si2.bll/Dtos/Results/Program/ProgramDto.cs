using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace si2.bll.Dtos.Results.Program
{
    public class ProgramDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string NameFr { get; set; }

        public string NameAr { get; set; }

        public string NameEn { get; set; }

        public Guid ProgramlevelId { get; set; }

        public Guid InstitutionId { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
