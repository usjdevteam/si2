using si2.dal.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static si2.common.Enums;
namespace si2.dal.Entities
{
    [Table("Cohort")]
    public class Cohort : Si2BaseDataEntity<Guid>, IAuditable
    {
        /*
         * promotion : string - mandatory - 20
         * programId : Guid - mandatory
         * courseCohorts : List<CourseCohort> - optional
         * userCohorts : List<UserCohort> - optional
         */

        [Required]
        [StringLength(20, ErrorMessage = "Promotion field must be equal or below 20 characters")]
        public string Promotion { get; set; }

        [Required]
        [ForeignKey("Program")]
        public Guid ProgramId { get; set; }

        //public Program Program { get; set; }

        //public ICollection<CourseCohort> CourseCohorts { get; set; }
        
        public ICollection<UserCohort> UserCohorts { get; set; }

        public static object Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}
