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
using WebShop.Domain.DTO.Users;
using WebShop.Domain.Entities;

namespace WebShop.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/userlockout")]
    public class UserLockoutApiController : Controller
    {
        private readonly UserStore<User> _userStore;

        public UserLockoutApiController(WebShopContext context)
        {
            _userStore = new UserStore<User>(context) { AutoSaveChanges = true };
        }

        [HttpPost("getEndDate")]
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync([FromBody]User user)
        {
            return await _userStore.GetLockoutEndDateAsync(user);
        }

        [HttpPost("setEndDate")]
        public Task SetLockoutEndDateAsync([FromBody]SetLockoutDto setLockoutDto)
        {
            return _userStore.SetLockoutEndDateAsync(setLockoutDto.User, setLockoutDto.LockoutEnd);
        }

        [HttpPost("IncrementAccessFailedCount")]
        public async Task<int> IncrementAccessFailedCountAsync([FromBody]User user)
        {
            return await _userStore.IncrementAccessFailedCountAsync(user);
        }

        [HttpPost("ResetAccessFailedCount")]
        public Task ResetAccessFailedCountAsync([FromBody]User user)
        {
            return _userStore.ResetAccessFailedCountAsync(user);
        }

        [HttpPost("GetAccessFailedCount")]
        public async Task<int> GetAccessFailedCountAsync([FromBody]User user)
        {
            return await _userStore.GetAccessFailedCountAsync(user);
        }

        [HttpPost("enabled")]
        public async Task<bool> GetLockoutEnabledAsync([FromBody]User user)
        {
            return await _userStore.GetLockoutEnabledAsync(user);
        }

        [HttpPost("enabled/{enabled}")]
        public async Task SetLockoutEnabledAsync([FromBody]User user, bool enabled)
        {
            await _userStore.SetLockoutEnabledAsync(user, enabled);
        }

    }
}