using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Domain.Entities;

namespace WebShop.Interfaces.Services
{
    public interface IUsersClient : 
        
        
       
        
        IUserLockoutStore<User>
    {
    }
}
