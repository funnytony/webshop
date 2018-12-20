using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Context;
using WebShop.Domain.Entities;

namespace WebShop.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/userstore")]
    public class UserStoreApiController : Controller, IUserStore<User>
    {
        private readonly UserStore<User> _userStore;

        public UserStoreApiController(WebShopContext context) => _userStore = new UserStore<User>(context) { AutoSaveChanges = true };

        #region IUserStrore
        [HttpPost("id")]
        public async Task<string> GetUserIdAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            return await _userStore.GetUserIdAsync(user);
        }

        [HttpPost("name")]
        public async Task<string> GetUserNameAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            return await _userStore.GetUserNameAsync(user);
        }

        [HttpPost("name/{userName}")]
        public async Task SetUserNameAsync([FromBody]User user, string userName, CancellationToken cancellationToken)
        {
            await _userStore.SetUserNameAsync(user, userName);
        }

        [HttpPost("normalName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            var result = await _userStore.GetNormalizedUserNameAsync(user);
            return result;
        }

        [HttpPost("normalName/{normalizedName}")]
        public async Task SetNormalizedUserNameAsync([FromBody]User user, string normalizedName, CancellationToken cancellationToken)
        {
            await _userStore.SetNormalizedUserNameAsync(user, normalizedName);
        }

        [HttpPost]
        public async Task<IdentityResult> CreateAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            var result = await _userStore.CreateAsync(user);
            return result;

        }

        [HttpPut]
        public async Task<IdentityResult> UpdateAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            var result = await _userStore.UpdateAsync(user);
            return result;
        }

        [HttpPost("delete")]
        public async Task<IdentityResult> DeleteAsync([FromBody]User user, CancellationToken cancellationToken)
        {
            var result = await _userStore.DeleteAsync(user);
            return result;
        }

        [HttpGet("{userId}")]
        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _userStore.FindByIdAsync(userId);
            return result;

        }

        [HttpGet("name/{normalizedUserName}")]
        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var result = await _userStore.FindByNameAsync(normalizedUserName);
            return result;
        }

        #endregion
    }
}