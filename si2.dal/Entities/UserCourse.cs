using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static si2.common.Enums;
namespace si2.dal.Entities
{
    [Table("UserCourse")]
    public class UserCourse : Si2BaseDataEntity<Guid>, IAuditable
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
