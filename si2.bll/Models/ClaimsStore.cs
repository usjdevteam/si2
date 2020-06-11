using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace si2.bll.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim ("Create Role", null),
            new Claim ("Edit Role", null),
            new Claim ("Delete Role", null),
            new Claim ("Create User Account" ,null),
            new Claim ("Update User Account" ,null)
        };
    }
}
