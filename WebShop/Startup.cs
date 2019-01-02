using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebShop.Logger;
using WebShop.Clients;
using WebShop.Clients.Services.Orders;
using WebShop.Clients.Services.Product;
using WebShop.Clients.Services.Users;
using WebShop.Domain.Entities;
using WebShop.Infrastructure.Implementations;
using WebShop.Infrastructure.Interfaces;
using WebShop.Interfaces;
using WebShop.Interfaces.Clients;
using WebShop.Services;
using WebShop.Services.Interfaces;
using WebShop.Services.Middleware;

namespace WebShop
{
    public class Startup
    {
        public IConfiguration Configuration { get;  }

        public Startup(IConfiguration configuration) => Configuration = configuration;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {            
            //string connection = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<WebShopContext>(options => options.UseSqlServer(connection));

            services.AddIdentity<User, IdentityRole>()                
                .AddDefaultTokenProviders();

            services.AddTransient<IUserStore<User>, UserStoreClient>();            

            services.AddTransient<IUserRoleStore<User>, UserRoleClient>();
            services.AddTransient<IUserClaimStore<User>, UserClaimClient>();
            services.AddTransient<IUserPasswordStore<User>, UserPasswordClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UserTwoFactorClient>();
            services.AddTransient<IUserEmailStore<User>, UserEmailClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UserPhoneNumberClient>();
            services.AddTransient<IUserLoginStore<User>, UserLoginClient>();
            services.AddTransient<IUserLockoutStore<User>, UserLockoutClient>();

            services.AddTransient<IRoleStore<IdentityRole>, RolesClient>();

            //конфигурируем Identity
            services.Configure<IdentityOptions>(options => {
                options.Password.RequiredLength = 6;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                //options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });

            //конфигурируем Cookie
            services.ConfigureApplicationCookie(options => {

                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);

                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";

                options.SlidingExpiration = true;
            });

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddTransient<IProductData, ProductsClient>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICartService, CookieCartService>();
            services.AddTransient<IOrderService, OrdersClient>();

            services.AddTransient<IValuesService, ValuesClient>();



            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider svp, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseStatusCodePagesWithRedirects("~/error/errorstatus/{0}");

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );

                routes.MapRoute(
                    name: "defoult",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            }
            );


        }

        
    }
}
