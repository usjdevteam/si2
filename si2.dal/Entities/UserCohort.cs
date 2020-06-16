﻿using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static si2.common.Enums;
namespace si2.dal.Entities
{
    [Table("UserCohort")]
    //public class UserCohort :  IAuditable
    public class UserCohort : Si2BaseDataEntity<Guid>, IAuditable
    {
        //[ForeignKey("User")]
        //public Guid UserId { get; set; }
        //public ApplicationUser User { get; set; }
        //public string User { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Cohort")]
        public Guid CohortId { get; set; }
        public Cohort Cohort { get; set; }
    }
}
