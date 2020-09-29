using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Utils.Enums
{
    public enum EnumUserStatus
    {
        Pending = 0,
        Active,
        LockedOut,
        Closed,
        Banned
    }
}