using IdentityManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityManagement.ViewModels
{
    public class UserAccountViewModel
    {
        public UserAccount UserAccount { get; set; }
        public IList<UserRole> UserRoles { get; set; }
    }
}