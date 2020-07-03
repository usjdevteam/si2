using si2.dal.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace si2.dal.Entities
{
    [Table("UserCohort")]
    public class UserCohort : Si2BaseDataEntity<Guid>, IAuditable
    {  
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("CohortId")]
        public Cohort Cohort { get; set; }
        public Guid CohortId { get; set; }
        
    }
}
