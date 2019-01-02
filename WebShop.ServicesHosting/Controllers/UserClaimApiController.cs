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
using WebShop.Domain.DTO.Users;
using WebShop.Domain.Entities;

namespace WebShop.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/userclaim")]
    public class UserClaimApiController : Controller
    {
        private readonly UserStore<User> _userStore;

        public UserClaimApiController(WebShopContext context)
        {
            _userStore = new UserStore<User>(context) { AutoSaveChanges = true };
        }

        [HttpPost("claims")]
        public async Task<IList<Claim>> GetClaimsAsync([FromBody]User user)
        {
            return await _userStore.GetClaimsAsync(user);
        }

        [HttpPost]
        public async Task AddClaimsAsync([FromBody]AddClaimsDto claimsDto)
        {
            await _userStore.AddClaimsAsync(claimsDto.User, claimsDto.Claims);
        }

        [HttpPost("replace")]
        public async Task ReplaceClaimAsync([FromBody]ReplaceClaimsDto claimsDto)
        {
            await _userStore.ReplaceClaimAsync(claimsDto.User, claimsDto.Claim, claimsDto.NewClaim);
        }

        [HttpPost("remove")]
        public async Task RemoveClaimsAsync([FromBody]RemoveClaimsDto claimsDto)
        {
            await _userStore.RemoveClaimsAsync(claimsDto.User, claimsDto.Claims);
        }

        [HttpPost("users")]
        public async Task<IList<User>> GetUsersForClaimAsync([FromBody]Claim claim)
        {
            return await _userStore.GetUsersForClaimAsync(claim);
        }

    }
}