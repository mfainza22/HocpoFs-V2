using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Entities
{
    public class Role 
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string RoleDesc { get; set; }

        public int DisplayOrder{ get; set; }

        public bool IsActive
        {
            get; set;
        }

    }
}