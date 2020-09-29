using System.Collections.Generic;
using DataAccessLayer;
using System.Text;
using WeighingSystemCoreHelpers.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityManagement.Entities;
using IdentityManagement.ViewModels;
using System.Linq;

namespace WeighingSystemCore.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        public UserRoleRepository()
        {

        }

        public void AddUserRole(string id, string roleName)
        {
            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = "UserAccountId", ParameterValue = id });
            parameters.Add(new ParameterInfo() { ParameterName = "RoleId", ParameterValue = roleName });

            StringBuilder str = new StringBuilder();
            str.AppendLine(" if ((select count(*) from UserRoles where UserAccountId = @UserAccountId and RoleId = @RoleId) = 0)");
            str.AppendLine(" begin");
            str.AppendLine(" Insert into UserRoles  ");
            str.AppendLine(" (UserAccountId,RoleId)  ");
            str.AppendLine(" values  ");
            str.AppendLine(" (@UserAccountId,@RoleId) ");
            str.AppendLine(" end");

            int success = DBContext.ExecuteQuery(str.ToString(), parameters, commandType: System.Data.CommandType.Text);
        }

        public UserRole GetUserRoleByName(string id, string roleName)
        {
            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = "UserAccountId", ParameterValue = id });
            parameters.Add(new ParameterInfo() { ParameterName = "RoleId", ParameterValue = roleName });

            StringBuilder str = new StringBuilder();
            str.AppendLine("Select * from UserRoles  ");
            str.AppendLine(" where UserAccountId = @UserAccountId and RoleId = @RoleId  ");

            var ur = DBContext.GetRecord<UserRole>(str.ToString(), parameters, commandType: System.Data.CommandType.Text);

            return ur;
        }


        public void DeleteUserRole(string id, string roleName)
        {
            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = "UserAccountId", ParameterValue = id });
            parameters.Add(new ParameterInfo() { ParameterName = "RoleId", ParameterValue = roleName });

            StringBuilder str = new StringBuilder();
            str.AppendLine(" Delete from UserRoles  ");
            str.AppendLine(" where UserAccountId = @UserAccountId and RoleId = @RoleId  ");
            int success = DBContext.ExecuteQuery(str.ToString(), parameters, commandType: System.Data.CommandType.Text);
        }

        public List<UserRole> ListUserRole(string id)
        {
            return new List<UserRole>();
        }

        public List<UserRole> ListUserRoleOuter(string id)
        {
            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = "UserAccountId", ParameterValue = id });

            StringBuilder str = new StringBuilder();
            str.AppendLine(" Select  ");
            str.AppendLine(" R.id as RoleId, ");
            str.AppendLine(" R.Name as RoleName, ");
            str.AppendLine(" R.RoleDesc, ");
            str.AppendLine(" UR.UserRoleId, ");
            str.AppendLine(" UR.UserAccountId,(case when (@UserAccountId is null or @UserAccountId = '') then 0 else case when (UR.UserRoleId is null) then 0 else 1 end end) as Granted, ");
            str.AppendLine(" R.DisplayOrder	 ");
            str.AppendLine(" from (Select * from Roles where IsActive = 1) as R  ");
            str.AppendLine(" left outer join (select * from UserRoles where UserAccountId =  @UserAccountId ) as UR on UR.RoleId = R.Id ");
            str.AppendLine(" order by R.DisplayOrder asc");

            var userroles = DBContext.GetRecords<UserRole>(str.ToString(), parameters, commandType: System.Data.CommandType.Text);
            return userroles;
        }

        public List<string> ListUserRoleName(string id)
        {
            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = "UserAccountId", ParameterValue = id });

            StringBuilder str = new StringBuilder();
            str.AppendLine(" Select Name from UserRoles as UR inner join Roles as R on UR.RoleId = R.Id ");
            str.AppendLine(" where UserAccountId = @UserAccountId");

            List<string> roles = DBContext.GetRecords<string>(str.ToString(), parameters, commandType: System.Data.CommandType.Text);
            return roles;
        }
    }
}