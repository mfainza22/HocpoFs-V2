using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityManagement.Entities;
using IdentityManagement.ViewModels;
using Microsoft.AspNetCore.Http;

public interface IUserAccountRepository
{
    UserAccount GetUserById(string id);
    UserAccount GetUserByName(string id);
    IEnumerable<UserAccount> ListUserAccounts(string qry = "");
    UserAccount CreateUserAccount(UserAccount UserAccount);
    void UpdateUserAccount(UserAccount UserAccountChanges);
    void DeleteUserAccount(UserAccount userAccount);
    void BulkDeleteUserAccount(string[] ids);
    UserAccountViewModel GetUserAccountDefault(string currentId = "");
    UserAccount GetLoggedInUser(HttpContext httpContext);
    string GetLoggedInUserId();

}