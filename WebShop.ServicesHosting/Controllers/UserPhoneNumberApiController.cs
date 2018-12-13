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
    [Route("api/userphonenumber")]
    public class UserPhoneNumberApiController : Controller
    {
        private readonly UserStore<User> _userStore;

        public UserPhoneNumberApiController(WebShopContext context)
        {
            _userStore = new UserStore<User>(context) { AutoSaveChanges = true };
        }

        [HttpPost("{phoneNumber}")]
        public async Task SetPhoneNumberAsync([FromBody]User user, string phoneNumber)
        {
            await _userStore.SetPhoneNumberAsync(user, phoneNumber);
        }

        [HttpPost]
        public async Task<string> GetPhoneNumberAsync([FromBody]User user)
        {
            return await _userStore.GetPhoneNumberAsync(user);
        }

        [HttpPost("confirmed")]
        public async Task<bool> GetPhoneNumberConfirmedAsync([FromBody]User user)
        {
            return await _userStore.GetPhoneNumberConfirmedAsync(user);
        }

        [HttpPost("confirmed/{confirmed}")]
        public async Task SetPhoneNumberConfirmedAsync([FromBody]User user, bool confirmed)
        {
            await _userStore.SetPhoneNumberConfirmedAsync(user, confirmed);
        }
    }
}