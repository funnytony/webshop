using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebShop.DAL.Context;

namespace WebShop.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/roles")]
    public class RolesApiController : Controller
    {
        private readonly RoleStore<IdentityRole> _roleStore;

        public RolesApiController(WebShopContext context) => _roleStore = new RoleStore<IdentityRole>(context);

        [HttpPost]
        public async Task<bool> CreateAsync(IdentityRole role)
        {
            var result = await _roleStore.CreateAsync(role);
            return result.Succeeded;
        }

        [HttpPut]
        public async Task<bool> UpdateAsync(IdentityRole role)
        {
            var result = await _roleStore.UpdateAsync(role);
            return result.Succeeded;
        }

        [HttpPost("delete")]
        public async Task<bool> DeleteAsync(IdentityRole role)
        {
            var result = await _roleStore.DeleteAsync(role);
            return result.Succeeded;
        }

        [HttpPost("id")]
        public async Task<string> GetRoleIdAsync([FromBody]IdentityRole role)
        {
            var result = await _roleStore.GetRoleIdAsync(role);
            return result;
        }

        [HttpPost("name")]
        public async Task<string> GetRoleNameAsync([FromBody]IdentityRole role)
        {
            var result = await _roleStore.GetRoleNameAsync(role);
            return result;
        }

        [HttpPost("name/{roleName}")]
        public Task SetRoleNameAsync([FromBody]IdentityRole role, string roleName)
        {
            return _roleStore.SetRoleNameAsync(role, roleName);
        }

        [HttpPost("normalName")]
        public async Task<string> GetNormalizedRoleNameAsync([FromBody]IdentityRole role)
        {
            var result = await _roleStore.GetRoleNameAsync(role);
            return result;
        }

        [HttpPost("normalName/{normalizedName}")]
        public Task SetNormalizedRoleNameAsync([FromBody]IdentityRole role, string normalizedName)
        {
            return _roleStore.SetNormalizedRoleNameAsync(role, normalizedName);
        }

        [HttpGet("{roleId}")]
        public async Task<IdentityRole> FindByIdAsync(string roleId)
        {
            var result = await _roleStore.FindByIdAsync(roleId);
            return result;
        }

        [HttpGet("name/{normalizedRoleName}")]
        public async Task<IdentityRole> FindByNameAsync(string normalizedRoleName)
        {
            var result = await _roleStore.FindByNameAsync(normalizedRoleName);
            return result;
        }

    }
}