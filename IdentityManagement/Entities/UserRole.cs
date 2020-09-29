using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Entities
{
    public class UserRole
    {
        public UserRole()
        {
            Granted = false;
        }
        public long UserRoleId { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public string RoleDesc { get; set; }

        [Display(Name = "Granted")]
        public bool Granted { get; set; }

        [Display(Name = "Order")]
        public int DisplayOrder { get; set; }
    }
}