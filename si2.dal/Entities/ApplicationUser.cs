using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ICollection<UserCohort> _userCohorts;
        public ICollection<UserCohort> UserCohorts 
        {
          get { return _userCohorts ?? (_userCohorts = new Collection<UserCohort>()); }
          set { _userCohorts = value; }
        }

        private ICollection<UserCourse> _userCourses;
        public ICollection<UserCourse> UserCourses
        {
            get { return _userCourses ?? (_userCourses = new Collection<UserCourse>()); }
            set { _userCourses = value; }
        }
    }
}


