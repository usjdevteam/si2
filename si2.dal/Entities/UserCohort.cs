using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static si2.common.Enums;

namespace si2.dal.Entities
{
    [Table("UserCohort")]
    public class UserCohort : Si2BaseDataEntity<Guid>, IAuditable
    {  
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public Guid CohortId { get; set; }
        [ForeignKey("CohortId")]
        public Cohort Cohort { get; set; }
       
        
    }
}
