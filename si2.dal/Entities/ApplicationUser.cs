using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace si2.dal.Entities
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        [MaxLength(50)]
        public string FirstNameFr { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastNameFr { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstNameAr { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastNameAr { get; set; }


        public ICollection<UserCohort> UserCohorts { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
    }
}


