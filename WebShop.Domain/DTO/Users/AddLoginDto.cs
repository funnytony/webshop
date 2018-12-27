using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Domain.Entities;

namespace WebShop.Domain.DTO.Users
{
    public class AddLoginDto
    {
        public User User { get; set; }

        public UserLoginInfo UserLoginInfo { get; set; }
    }
}
