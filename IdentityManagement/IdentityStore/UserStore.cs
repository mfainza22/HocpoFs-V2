using DataAccessLayer;
using IdentityManagement.Entities;
using IdentityManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityManagement.IdentityStore
{
    public class UserStore : IUserStore<UserAccount>, IUserRoleStore<UserAccount>, IUserPhoneNumberStore<UserAccount>,
    IUserTwoFactorStore<UserAccount>, IUserPasswordStore<UserAccount>
    {
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IUserAccountRepository userAccountRepository;

        public UserStore(IUserRoleRepository _userRoleRepository, IUserAccountRepository _userAccountRepository)
        {
            userRoleRepository = _userRoleRepository;
            userAccountRepository = _userAccountRepository;
        }
        #region IUserStore
        public async Task<IdentityResult> CreateUser(UserAccount user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user != null)
            {
                await Task<IdentityResult>.Factory.StartNew(() =>
                {
                    userAccountRepository.CreateUserAccount(user);
                    return IdentityResult.Success;
                });

                return IdentityResult.Success;

            }
            else
            {
                var ie = new IdentityError();
                ie.Code = nameof(UserAccount.Id);
                ie.Description = "Failed to create new user";
                return IdentityResult.Failed(new IdentityError[] { ie });
            }
        }

        public async void DeleteAsync(UserAccount user, CancellationToken cancellation)
        {
            if (user != null)
            {
                await Task<IdentityResult>.Factory.StartNew(() =>
                {
                    userAccountRepository.DeleteUserAccount(user);
                    return IdentityResult.Success;
                });
            }
            throw new ArgumentNullException("user");
        }

        public void Dispose()
        {

        }
        public async Task<UserAccount> FindByIdAsync(string userId, CancellationToken cancellation)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await Task.Factory.StartNew(() =>
                {
                    return userAccountRepository.GetUserById(userId);
                });
            }

            throw new ArgumentNullException("userId");
        }

        public async Task<UserAccount> FindByNameAsync(string userName, CancellationToken cancellation)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                return await Task.Factory.StartNew(() =>
              {
                  return userAccountRepository.GetUserByName(userName);
              });

            }
            throw new ArgumentNullException("userName");
        }


        #endregion

        #region IUserRoleStore


        public Task RemoveFromRoleAsync(UserAccount user, string roleName)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    userRoleRepository.DeleteUserRole(user.Id, roleName);
                });
            }
            else
            {
                throw new ArgumentNullException("user");
            }
        }



        public Task<bool> IsInRoleAsync(UserAccount user, string roleName)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    IList<string> roles = userRoleRepository.ListUserRoleName(user.Id);
                    foreach (string role in roles)
                    {
                        if (role.ToUpper() == roleName.ToUpper())
                        {
                            return true;
                        }
                    }

                    return false;
                });
            }
            else
            {
                throw new ArgumentNullException("User not found.");
            }
        }

        public Task<string> GetUserIdAsync(UserAccount user, CancellationToken cancellationToken)
        {
            return Task<string>.Factory.StartNew(() =>
                 {
                     return user.Id;
                 });
        }

        public Task<string> GetUserNameAsync(UserAccount user, CancellationToken cancellationToken)
        {
            return Task<string>.Factory.StartNew(() =>
                 {
                     return user.UserName;
                 });
        }

        public Task SetUserNameAsync(UserAccount user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(UserAccount user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(UserAccount user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IdentityResult> IUserStore<UserAccount>.CreateAsync(UserAccount user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                           {
                               userAccountRepository.CreateUserAccount(user);

                               return IdentityResult.Success;
                           });

            }
            else
            {
                throw new ArgumentNullException("Failed to create new");
            }
        }

        Task<IdentityResult> IUserStore<UserAccount>.UpdateAsync(UserAccount user, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                           {
                               userAccountRepository.UpdateUserAccount(user);
                               return IdentityResult.Success;
                           });
            }
            else
            {
                throw new ArgumentNullException("Failed to update user");
            }
        }

        Task<IdentityResult> IUserStore<UserAccount>.DeleteAsync(UserAccount user, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                           {
                               userAccountRepository.DeleteUserAccount(user);
                               return IdentityResult.Success;
                           });
            }
            else
            {
                throw new ArgumentNullException("Failed to update user");
            }
        }

        public Task AddToRoleAsync(UserAccount user, string roleName, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
                 {
                     userRoleRepository.AddUserRole(user.Id, roleName);
                 });
        }

        public Task RemoveFromRoleAsync(UserAccount user, string roleName, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
                {
                    userRoleRepository.DeleteUserRole(user.Id, roleName);
                });
        }

        public Task<IList<string>> GetRolesAsync(UserAccount user, CancellationToken cancellationToken)
        {
            if (user != null)
            {
                return Task.Factory.StartNew(() =>
                {
                    IList<string> roles = userRoleRepository.ListUserRoleName(user.Id);
                    return roles;
                });
            }
            else
            {
                throw new ArgumentNullException("An error encountered getting roles for user");
            }
        }

        public Task<bool> IsInRoleAsync(UserAccount user, string roleName, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
               {
                   var ur = userRoleRepository.GetUserRoleByName(user.Id, roleName);
                   return ur != null;
               });
        }

        public Task<IList<UserAccount>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberAsync(UserAccount user, string phoneNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPhoneNumberAsync(UserAccount user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(UserAccount user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(UserAccount user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(UserAccount user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(UserAccount user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(UserAccount user, string passwordHash, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
              {
                  user.UserPwdHash = passwordHash;
              });
        }

        public Task<string> GetPasswordHashAsync(UserAccount user, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
               {
                   var ur = user.UserPwdHash;
                   return ur;
               });
        }

        public Task<bool> HasPasswordAsync(UserAccount user, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
               {
                   var ur = user.UserPwdHash;
                   return ur != null;
               });
        }


        #endregion

    }
}