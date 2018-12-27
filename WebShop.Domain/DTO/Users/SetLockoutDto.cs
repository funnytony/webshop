using System;
using WebShop.Domain.Entities;

namespace WebShop.Domain.DTO.Users
{
    public class SetLockoutDto
    {
        public User User { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

    }
}
