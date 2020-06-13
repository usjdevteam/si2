using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace si2.dal.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public ICollection<UserCohort> UserCohorts { get; set; }
    }
}
