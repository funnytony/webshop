using System.Collections.Generic;
using System.Security.Claims;
using WebShop.Domain.Entities;

namespace WebShop.Domain.DTO.Users
{
    public class AddClaimsDto
    {
        public User User { get; set; }

        public IEnumerable<Claim> Claims { get; set; }
    }
}
