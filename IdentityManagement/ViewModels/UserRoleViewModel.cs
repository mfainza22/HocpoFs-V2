using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.ViewModels
{
    public class UserRoleViewModel1
    {

        [Display(Name ="User Role I.D.")]
        public long UserRoleId{get; set;}

        public string UserAccountId{get; set;}

        public string RoleId { get; set; }

        [Display(Name ="Role Name")]
        public string RoleName { get; set; }

        [Display(Name ="RoleDesc")]

        public string RoleDesc { get; set; }

        [Display(Name = "Granted")]
        public bool Granted { get; set; }

        [Display(Name = "Order")]
        public int DisplayOrder { get; set; }

    }
}
