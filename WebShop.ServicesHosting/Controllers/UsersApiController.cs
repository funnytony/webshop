using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Context;
using WebShop.Domain.Entities;
using WebShop.Interfaces.Services;

namespace WebShop.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersApiController : Controller
    {
        private readonly UserStore<User> _userStore;

        public UsersApiController(WebShopContext context) => _userStore = new UserStore<User>(context) { AutoSaveChanges = true };

        #region IUserStrore
        [HttpPost("userId")]
        public async Task<string> GetUserIdAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            return await _userStore.GetUserIdAsync(user);
        }

        [HttpPost("userName")]
        public async Task<string> GetUserNameAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            return await _userStore.GetUserNameAsync(user);
        }

        [HttpPost("userName/{userName}")]
        public async Task SetUserNameAsync([FromBody]User user, string userName, CancellationToken cancellationToken)
        {
            await _userStore.SetUserNameAsync(user, userName);
        }

        [HttpPost("normalUserName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            var result = await _userStore.GetNormalizedUserNameAsync(user);
            return result;
        }

        [HttpPost("normalUserName/{normalizedName}")]
        public async Task SetNormalizedUserNameAsync([FromBody]User user, string normalizedName, CancellationToken cancellationToken)
        {
            await _userStore.SetNormalizedUserNameAsync(user, normalizedName);
        }

        [HttpPost("user")]
        public async Task<IdentityResult> CreateAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            var result = await _userStore.CreateAsync(user);
            return result;

        }

        [HttpPut("user")]
        public async Task<IdentityResult> UpdateAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            var result = await _userStore.UpdateAsync(user);
            return result;
        }

        [HttpPost("user/delete")]
        public async Task<IdentityResult> DeleteAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            var result = await _userStore.DeleteAsync(user);
            return result;
        }

        [HttpGet("user/find/{userId}")]
        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _userStore.FindByIdAsync(userId);
            return result;

        }

        [HttpGet("user/normal/{normalizedUserName}")]
        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var result = await _userStore.FindByNameAsync(normalizedUserName);
            return result;
        }

        #endregion

        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        

        public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        
        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        

       

        public Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        
        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        

    }
}