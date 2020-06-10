using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace si2.dal.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstNameFr { get; set; }
        public string LastNameFr { get; set; }
        public string FirstNameAr { get; set; }
        public string LastNameAr { get; set; }
    }
}


