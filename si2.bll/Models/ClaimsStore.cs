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
            new Claim ("Create Role", string.Empty),
            new Claim ("Edit Role", string.Empty),
            new Claim ("Delete Role", string.Empty),
            new Claim ("Create User Account" ,string.Empty),
            new Claim ("Update User Account" ,string.Empty)
        };
    }
}
