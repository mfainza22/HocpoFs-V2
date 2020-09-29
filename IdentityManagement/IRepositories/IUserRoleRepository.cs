using System;
using System.Collections.Generic;
using IdentityManagement.Entities;
using IdentityManagement.ViewModels;

public interface IUserRoleRepository
{
    List<UserRole> ListUserRole(string id);
    List<UserRole> ListUserRoleOuter(string id);
    List<string> ListUserRoleName(string id);
     UserRole GetUserRoleByName(string id, string roleName);
    void AddUserRole(string id, string roleName);
    void DeleteUserRole(string id, string roleName);

}