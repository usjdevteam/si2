using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace si2.dal.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name, Guid institutionId) : base(name)
        {
            this.InstitutionId = institutionId;
        }
        public Guid? InstitutionId { get; set; }
    }
}
