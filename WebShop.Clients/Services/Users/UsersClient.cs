using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebShop.Clients.Base;
using WebShop.Domain.DTO.Users;
using WebShop.Domain.Entities;
using WebShop.Interfaces.Services;

namespace WebShop.Clients.Services.Users
{
    public class UsersClient: BaseClient, IUsersClient
    {
        private readonly IUserStoreClient _userStoreClient;

        public UsersClient(IConfiguration configuration, IUserStoreClient userStoreClient) : base(configuration)
        {
            _userStoreClient = userStoreClient;
            ServiceAddress = "api/users";
        }

        protected sealed override string ServiceAddress { get; set; }

        #region IUserStore
        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.GetUserIdAsync(user, cancellationToken);
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.GetUserNameAsync(user, cancellationToken);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            return _userStoreClient.SetUserNameAsync(user, userName, cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return _userStoreClient.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.CreateAsync(user, cancellationToken);
        }


        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.UpdateAsync(user, cancellationToken);
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            return await _userStoreClient.DeleteAsync(user, cancellationToken);
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _userStoreClient.FindByIdAsync(userId, cancellationToken);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _userStoreClient.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        #endregion

        #region IUserPasswordStore
        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            var url = $"{ServiceAddress}/setPasswordHash";
            await PostAsync(url, new PasswordHashDto() { User = user, Hash = passwordHash });
        }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getPasswordHash";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<string>();
            return ret;
        }

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/hasPassword";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        #endregion

        #region IUserClaimStore
        public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getClaims";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<List<Claim>>();
        }

        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/addClaims";
            return PostAsync(url, new AddClaimsDto() { User = user, Claims = claims });
        }

        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/replaceClaim";
            return PostAsync(url, new ReplaceClaimsDto() { User = user, Claim = claim, NewClaim = newClaim });
        }

        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/removeClaims";
            return PostAsync(url, new RemoveClaimsDto() { User = user, Claims = claims });
        }

        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getUsersForClaim";
            var result = await PostAsync(url, claim);
            return await result.Content.ReadAsAsync<List<User>>();
        }

        #endregion

        #region IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            var url = $"{ServiceAddress}/setTwoFactor/{enabled}";
            return PostAsync(url, user);
        }

        public async Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getTwoFactorEnabled";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        #endregion

        #region IUserEmailStore

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            var url = $"{ServiceAddress}/setEmail/{email}";
            return PostAsync(url, user);
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getEmail";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getEmailConfirmed";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            var url = $"{ServiceAddress}/setEmailConfirmed/{confirmed}";
            return PostAsync(url, user);
        }

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/findByEmail/{normalizedEmail}";
            return GetAsync<User>(url);
        }

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getNormalizedEmail";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            var url = $"{ServiceAddress}/setEmail/{normalizedEmail}";
            return PostAsync(url, user);
        }

        #endregion

        #region IUserPhoneNumberStore

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            var url = $"{ServiceAddress}/setPhoneNumber/{phoneNumber}";
            return PostAsync(url, user);
        }

        public async Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getPhoneNumber";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getPhoneNumberConfirmed";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            var url = $"{ServiceAddress}/setPhoneNumberConfirmed/{confirmed}";
            return PostAsync(url, user);
        }

        #endregion

        #region IUserLoginStore

        public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/addLogin";
            return PostAsync(url, new AddLoginDto() { User = user, UserLoginInfo = login });
        }

        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/removeLogin/{loginProvider}/{providerKey}";
            return PostAsync(url, user);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getLogins";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<List<UserLoginInfo>>();
        }

        public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/findbylogin/{loginProvider}/{providerKey}";
            return GetAsync<User>(url);
        }

        #endregion

        #region IUserLockoutStore

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getLockoutEndDate";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<DateTimeOffset?>();
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEnd = lockoutEnd;
            var url = $"{ServiceAddress}/setLockoutEndDate";
            return PostAsync(url, new SetLockoutDto() { User = user, LockoutEnd = lockoutEnd });
        }

        public async Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/IncrementAccessFailedCount";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<int>();
        }

        public Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/ResetAccessFailedCount";
            return PostAsync(url, user);
        }

        public async Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/GetAccessFailedCount";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<int>();
        }

        public async Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/GetLockoutEnabled";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        public async Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
            var url = $"{ServiceAddress}/SetLockoutEnabled/{enabled}";
            await PostAsync(url, user);
        }
        #endregion

        public void Dispose()
        {
            Client.Dispose();
        }


    }
}
