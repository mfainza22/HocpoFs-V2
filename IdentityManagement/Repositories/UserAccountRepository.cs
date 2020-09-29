using System.Collections.Generic;
using DataAccessLayer;
using System.Text;
using WeighingSystemCoreHelpers.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityManagement.Entities;
using IdentityManagement.ViewModels;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace WeighingSystemCore.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IUserRoleRepository userRoleRepository;
        private static cryptor c = new cryptor("JOJO");

        private static UserAccount CurrentUser { get; set; }

        public UserAccountRepository(IUserRoleRepository _userRoleRepository)
        {
            userRoleRepository = _userRoleRepository;
        }

        public UserAccount GetUserById([FromRoute] string id)
        {
            if (id == System.Guid.Empty.ToString() || string.IsNullOrEmpty(id)) return null;
            var result = new UserAccount();

            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(result.Id), ParameterValue = id, ParameterDbType = System.Data.DbType.String });

            StringBuilder str = new StringBuilder();
            str.AppendLine("Select * From UserAccounts where Id = @Id");

            result = DBContext.GetRecord<UserAccount>(str.ToString(), parameters, commandType: System.Data.CommandType.Text);
            if (result != null)
            {
                if (!string.IsNullOrEmpty(result.UserPwdHash))
                {
                    result.UserPwdString = c.decrypt(result.UserPwdHash);
                    result.UserPwdStringConfirm = result.UserPwdString;
                }
            }

            // result.UserAccount = IdentityUserController.GetUserById(id);
            // result.UserRoles = userRoleRepository.ListUserRoleOuter(result.UserAccount.Id);
            return result;
        }

        public UserAccount GetUserByName([FromRoute] string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var result = new UserAccount();

            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(UserAccount.UserName), ParameterValue = name, ParameterDbType = System.Data.DbType.String });

            StringBuilder str = new StringBuilder();
            str.AppendLine($"Select * From UserAccounts where {nameof(UserAccount.UserName)} = {nameof(UserAccount.UserName).Parameterize()}");

            result = DBContext.GetRecord<UserAccount>(str.ToString(), parameters, commandType: System.Data.CommandType.Text);
            if (result != null)
            {
                if (!string.IsNullOrEmpty(result.UserPwdHash))
                {
                    result.UserPwdString = c.decrypt(result.UserPwdHash);
                    result.UserPwdStringConfirm = result.UserPwdString;
                }
            }

            return result;
        }

        public IEnumerable<UserAccount> ListUserAccounts(string qry = "")
        {
            if (qry == string.Empty) qry = "Select Id,FullName,UserName,EmailAddress,PhoneNumber,Department,Position,DateCreated,IsActive,Status from UserAccounts";
            var results = DBContext.GetRecords<UserAccount>(qry.ToString());
            return results;
        }

        private TransactionQuery generateInserQuery(UserAccount objUser)
        {
            cryptor c = new cryptor("JOJO");
            List<ParameterInfo> parameters = new List<ParameterInfo>();

            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.Id), ParameterValue = objUser.Id });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.UserName), ParameterValue = objUser.UserName });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.UserPwdHash), ParameterValue = objUser.UserPwdHash });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.LastName), ParameterValue = objUser.LastName.ToUpper() });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.MiddleName), ParameterValue = objUser.MiddleName.ToUpper() });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.FirstName), ParameterValue = objUser.FirstName.ToUpper() });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.NameSuffix), ParameterValue = objUser.NameSuffix.ToUpper() });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.PhoneNumber), ParameterValue = objUser.PhoneNumber });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.EmailAddress), ParameterValue = objUser.EmailAddress });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.Position), ParameterValue = objUser.Position.ToUpper() });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.Department), ParameterValue = objUser.Department.ToUpper() });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.DateCreated), ParameterValue = objUser.DateCreated });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.RegisteredBy), ParameterValue = objUser.RegisteredBy });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.ModifiedBy), ParameterValue = objUser.ModifiedBy });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.IsActive), ParameterValue = objUser.IsActive });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.AccessFailedCount), ParameterValue = 0 });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.Status), ParameterValue = objUser.Status, ParameterDbType = System.Data.DbType.String });

            StringBuilder str = new StringBuilder();
            str.AppendLine(" -- SET NOCOUNT ON added to prevent extra result sets from ");
            str.AppendLine(" 	-- interfering with SELECT statements. ");
            str.AppendLine(" 	SET NOCOUNT ON; ");

            str.AppendLine(" Insert into UserAccounts ");
            str.AppendLine(" ( ");
            str.AppendLine(" Id, ");
            str.AppendLine(" UserName, ");
            str.AppendLine(" UserPwdHash, ");
            str.AppendLine(" LastName, ");
            str.AppendLine(" MiddleName, ");
            str.AppendLine(" FirstName, ");
            str.AppendLine(" NameSuffix, ");
            str.AppendLine(" Department, ");
            str.AppendLine(" Position, ");
            str.AppendLine(" EmailAddress, ");
            str.AppendLine(" PhoneNumber, ");
            str.AppendLine(" AccessFailedCount, ");
            str.AppendLine(" DateCreated, ");
            str.AppendLine(" IsActive, ");
            str.AppendLine(" Status ");
            str.AppendLine(" ) ");
            str.AppendLine("  VALUES  ");
            str.AppendLine(" ( ");
            str.AppendLine(" @Id, ");
            str.AppendLine(" @UserName, ");
            str.AppendLine(" @UserPwdHash, ");
            str.AppendLine(" @LastName, ");
            str.AppendLine(" @MiddleName, ");
            str.AppendLine(" @FirstName, ");
            str.AppendLine(" @NameSuffix, ");
            str.AppendLine(" @Department, ");
            str.AppendLine(" @Position, ");
            str.AppendLine(" @EmailAddress, ");
            str.AppendLine(" @PhoneNumber, ");
            str.AppendLine(" @AccessFailedCount, ");
            str.AppendLine(" @DateCreated, ");
            str.AppendLine(" @IsActive, ");
            str.AppendLine(" @Status ");
            str.AppendLine(" ) ");

            return new TransactionQuery() { Query = str, Parameters = parameters };
        }

        public UserAccount CreateUserAccount(UserAccount userAccount)
        {
            userAccount.DateCreated = DateTime.Now;

            userAccount.Id = Guid.NewGuid().ToString();
            if (!string.IsNullOrEmpty(userAccount.UserPwdString))
            {
                userAccount.UserPwdHash = c.encrypt(userAccount.UserPwdString);
            }

            var tQUery = generateInserQuery(userAccount);
            DBContext.ExecuteQuery(tQUery.Query.ToString(), tQUery.Parameters);

            return userAccount;
        }

        public void UpdateUserAccount(UserAccount userAccountChanges)
        {
            cryptor c = new cryptor("JOJO");

            var objUser = userAccountChanges;
            objUser.TimeStamp = DateTime.Now;
            objUser.UserPwdHash = c.encrypt(objUser.UserPwdString);

            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.Id), ParameterValue = objUser.Id, ParameterDbType = System.Data.DbType.String });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.UserName), ParameterValue = objUser.UserName });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.UserPwdHash), ParameterValue = objUser.UserPwdHash });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.LastName), ParameterValue = objUser.LastName.ToUpper() });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.MiddleName), ParameterValue = objUser.MiddleName.ToUpper() });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.FirstName), ParameterValue = objUser.FirstName.ToUpper() });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.NameSuffix), ParameterValue = objUser.NameSuffix.ToUpper() });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.PhoneNumber), ParameterValue = objUser.PhoneNumber });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.EmailAddress), ParameterValue = objUser.EmailAddress });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.Position), ParameterValue = objUser.Position.ToUpper() });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.Department), ParameterValue = objUser.Department.ToUpper() });
            //parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.DateCreated), ParameterValue = objUser.DateCreated });
            //parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.RegisteredBy), ParameterValue = objUser.RegisteredBy });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.ModifiedBy), ParameterValue = objUser.ModifiedBy });
            parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.TimeStamp), ParameterValue = objUser.TimeStamp });
            // parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.Status), ParameterValue = objUser.Status, ParameterDbType = System.Data.DbType.String });
            //parameters.Add(new ParameterInfo { ParameterName = nameof(objUser.AccessFailedCount), ParameterValue = 0 });


            StringBuilder str = new StringBuilder();
            str.AppendLine(" UPDATE [dbo].[UserAccounts] ");
            str.AppendLine($"    SET {nameof(objUser.UserName)} = @{nameof(objUser.UserName)}, ");
            str.AppendLine($"     {nameof(objUser.UserPwdHash)} = @{nameof(objUser.UserPwdHash)}, ");
            str.AppendLine($"     {nameof(objUser.LastName)} = @{nameof(objUser.LastName)}, ");
            str.AppendLine($"     {nameof(objUser.MiddleName)} = @{nameof(objUser.MiddleName)}, ");
            str.AppendLine($"     {nameof(objUser.FirstName)} = @{nameof(objUser.FirstName)}, ");
            str.AppendLine($"     {nameof(objUser.NameSuffix)} = @{nameof(objUser.NameSuffix)}, ");
            str.AppendLine($"     {nameof(objUser.Department)} = @{nameof(objUser.Department)}, ");
            str.AppendLine($"     {nameof(objUser.Position)} = @{nameof(objUser.Position)}, ");
            str.AppendLine($"     {nameof(objUser.EmailAddress)} = @{nameof(objUser.EmailAddress)}, ");
            str.AppendLine($"     {nameof(objUser.PhoneNumber)} = @{nameof(objUser.PhoneNumber)}, ");
            str.AppendLine($"     {nameof(objUser.ModifiedBy)} = @{nameof(objUser.ModifiedBy)}, ");
            str.AppendLine($"     {nameof(objUser.TimeStamp)} = @{nameof(objUser.TimeStamp)} ");
            str.AppendLine("  WHERE Id = @Id ");

            int success = DBContext.ExecuteQuery(str.ToString(), parameters: parameters, commandType: System.Data.CommandType.Text);
        }

        public void DeleteUserAccount(UserAccount objUser)
        {
            List<ParameterInfo> parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(objUser.Id), ParameterValue = objUser.Id, ParameterDbType = System.Data.DbType.String });

            if (objUser.Id == "cea78691-84a0-411b-b29d-89b17d8e4c69")
            {
                throw new Exception("This User cannot be deleted.");
            }

            StringBuilder str = new StringBuilder();
            str.AppendLine("Delete From UserAccounts where Id = @Id; Delete From UserRoles where UserAccountId = @Id");

            int result = DBContext.ExecuteQuery(str.ToString(), parameters, commandType: System.Data.CommandType.Text);
        }

        public void BulkDeleteUserAccount(string[] ids)
        {

            if (ids == null || ids.Count() == 0)
            {
                throw new Exception("Please select a user to delete.");
            }

            if (ids.Where(a => a == "cea78691-84a0-411b-b29d-89b17d8e4c69").Count() != 0)
            {
                throw new Exception("This user selected user cannot be deleted.");
            }

            StringBuilder str = new StringBuilder();
            for (int i = 0; i <= ids.Count() - 1; i++)
            {
                str.AppendLine($"Delete From UserAccounts where Id = '{ids[i]}'; Delete From UserRoles where UserAccountId = '{ids[i]}' ");
            }

            if (str.Length > 0)
            {
                DBContext.ExecuteQuery(str.ToString(), commandType: System.Data.CommandType.Text);
            }
        }


        public UserAccountViewModel GetUserAccountDefault(string currentId = "")
        {
            return new UserAccountViewModel();
        }


        public UserAccount GetLoggedInUser(HttpContext httpContext)
        { /// NOT WORKING
            var claimsPrincipal = (ClaimsPrincipal)httpContext.User;
            var userID = Convert.ToString(claimsPrincipal.Claims.ToList().Find(r => r.Type.Contains("nameidentifier")).Value);
            var result = GetUserById(userID);
            result.UserPwdHash = String.Empty;
            result.UserPwdHashConfirm = String.Empty;
            result.UserPwdStringConfirm = String.Empty;
            result.UserPwdString = String.Empty;
            return result;
        }


        public string GetLoggedInUserId()
        {
            return String.Empty;
            /// NOT WORKING
            // var claims = (ClaimsPrincipal)principal;
            // return Convert.ToString(claims.Claims.ToList().Find(r => r.Type == nameof(UserAccount.Id)).Value);
        }

    }
}