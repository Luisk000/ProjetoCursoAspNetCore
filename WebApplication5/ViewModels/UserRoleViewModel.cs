using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//IdentityUserRole

namespace WebApplication5.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        // public string UserRole { get; set; }
        public bool IsSelected { get; set; }
    }
}