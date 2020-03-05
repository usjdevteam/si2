using System;
using System.Collections.Generic;
using System.Text;

namespace si2.dal.Interfaces
{
    public interface IHasFullAudit
    { 
        string CreatedBy { get; set; }
        DateTime? CreatedOn { get; set; }
        string LastModifiedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }
        string DeletedBy { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}
