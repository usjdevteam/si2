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
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
       

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public Guid CourseId { get; set; }
    }
}
