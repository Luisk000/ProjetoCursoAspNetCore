using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            //(type, value)
            new Claim("Create Roles", "Create Roles"),
            new Claim("Edit Roles", "Edit Roles"),
            new Claim("Delete Roles", "Delete Roles"),
        };
    }
}
