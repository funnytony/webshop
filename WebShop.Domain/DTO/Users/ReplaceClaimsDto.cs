using System.Security.Claims;
using WebShop.Domain.Entities;

namespace WebShop.Domain.DTO.Users
{
    public class ReplaceClaimsDto
    {
        public User User { get; set; }

        public Claim Claim { get; set; }

        public Claim NewClaim { get; set; }

    }
}
