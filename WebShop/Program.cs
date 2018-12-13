using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebShop.DAL.Context;
using WebShop.Data;
using WebShop.Domain;
using WebShop.Domain.Entities;
using WebShop.Models;


namespace WebShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var context = services.GetRequiredService<WebShopContext>();
            //        DbInitializer.Initialize(context);

            //        var roleStore = new RoleStore<IdentityRole>(context);

            //        var roleManager = new RoleManager<IdentityRole>(roleStore,
            //            new IRoleValidator<IdentityRole>[] { },
            //            new UpperInvariantLookupNormalizer(),
            //            new IdentityErrorDescriber(), null);
            //        if(!roleManager.RoleExistsAsync("User").Result)
            //        {
            //            var role = new IdentityRole("User");
            //            var result = roleManager.CreateAsync(role).Result;
            //        }
            //        if (!roleManager.RoleExistsAsync("Administrator").Result)
            //        {
            //            var role = new IdentityRole("Administrator");
            //            var result = roleManager.CreateAsync(role).Result;
            //        }

            //        var userStore = new UserStore<User>(context);

            //        var userManager = new UserManager<User>(userStore, new OptionsManager<IdentityOptions>(new OptionsFactory<IdentityOptions>(new IConfigureOptions<IdentityOptions>[] { },
            //                new IPostConfigureOptions<IdentityOptions>[] { })),
            //            new PasswordHasher<User>(), new IUserValidator<User>[] { }, new IPasswordValidator<User>[] { },
            //            new UpperInvariantLookupNormalizer(), new IdentityErrorDescriber(), null, null);

            //        if (userStore.FindByNameAsync("Admin", CancellationToken.None).Result == null)
            //        {
            //            var user = new User() { UserName = "Admin", Email = "admin@gmail.com" };
            //            var result = userManager.CreateAsync(user, "admin").Result;
            //            if (result == IdentityResult.Success)
            //            {
            //                var roleResult = userManager.AddToRoleAsync(user, WebShopConstants.Roles.Admin).Result;
            //                var code = userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            //                var confirmed = userManager.ConfirmEmailAsync(user, code);
            //            }

            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred while seeding the database.");
            //    }

            //}
            host.Run();


        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().Build();
    }
}
