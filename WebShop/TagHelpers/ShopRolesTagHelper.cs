using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShop.TagHelpers
{
    [HtmlTargetElement(Attributes = "shop-roles")]
    public class ShopRolesTagHelper : TagHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _accessor;

        [HtmlAttributeName("shop-roles")]
        public string ShopRoles { get; set; }

        public ShopRolesTagHelper(UserManager<User> userManger, IHttpContextAccessor accessor)
        {
            _userManager = userManger;
            _accessor = accessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            CheckRole(output);
            //base.Process(context, output);
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            await Task.Run(() => { CheckRole(output); });

            //return base.ProcessAsync(context, output);
        }

        private void CheckRole(TagHelperOutput output)
        {
            var user = _userManager.GetUserAsync(_accessor.HttpContext.User).Result;
            if(ReferenceEquals(user, null))
            {
                output.SuppressOutput();
                return;
            }
            var role = _userManager.GetRolesAsync(user).Result;
            var tagRoles = ShopRoles.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(r=>r.Trim());
            if(!role.Any(r=>tagRoles.Contains(r)))
            {
                output.SuppressOutput();
            }
        }
    }
}
