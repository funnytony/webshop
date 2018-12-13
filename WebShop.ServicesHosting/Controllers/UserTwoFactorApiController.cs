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
    [Route("api/usertwofactor")]
    public class UserTwoFactorApiController : Controller
    {
        private readonly UserStore<User> _userStore;

        public UserTwoFactorApiController(WebShopContext context)
        {
            _userStore = new UserStore<User>(context) { AutoSaveChanges = true };
        }

        [HttpPost("{enabled}")]
        public async Task SetTwoFactorEnabledAsync([FromBody]User user, bool enabled)
        {
            await _userStore.SetTwoFactorEnabledAsync(user, enabled);
        }

        [HttpPost]
        public async Task<bool> GetTwoFactorEnabledAsync([FromBody]User user)
        {
            return await _userStore.GetTwoFactorEnabledAsync(user);
        }



    }
}