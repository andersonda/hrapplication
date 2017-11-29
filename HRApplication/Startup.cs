using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HRApplication.Data;
using HRApplication.Services;
using HRApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace HRApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<UserProfileContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<PositionContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<NotificationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NotificationContext")));


            services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireHR", policy => policy.RequireRole("HR"));
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizePage("/Index");
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                    options.Conventions.AuthorizeFolder("/Profile");
                    options.Conventions.AuthorizePage("/Profile/Create");
                    options.Conventions.AuthorizePage("/Profile/Edit");
                    options.Conventions.AuthorizeFolder("/Positions");
                    options.Conventions.AuthorizePage("/Positions/Create", "RequireHR");
                    options.Conventions.AuthorizePage("/Positions/Edit", "RequireHR");
                    options.Conventions.AuthorizePage("/Positions/Delete", "RequireHR");
                    options.Conventions.AuthorizeFolder("/Applications");
                    options.Conventions.AuthorizePage("/Positions/PositionApplications", "RequireHR");
                });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            
            CreateRolesAndUsers(serviceProvider).Wait();
        }

        private async Task CreateRolesAndUsers(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Manager", "HR"};
            IdentityResult roleResult;

            foreach(var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                // ensure that the role does not exist
                if (!roleExist)
                {
                    //create the roles and seed them to the database: 
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // find the user with the manager email
            var _manager = await UserManager.FindByEmailAsync("manager@company.com");
            if(_manager == null)
            {
                var manager = new ApplicationUser
                {
                    UserName = "manager@company.com",
                    Email = "manager@company.com",
                };

                string managerPassword = "6240ManagerP";
                var createManager = await UserManager.CreateAsync(manager, managerPassword);
                if (createManager.Succeeded)
                {
                    await UserManager.AddToRoleAsync(manager, "Manager");
                }
            }

            // find the user with the hr1 email
            var _hr1 = await UserManager.FindByEmailAsync("hr1@company.com");
            if (_hr1 == null)
            {
                var hr1 = new ApplicationUser
                {
                    UserName = "hr1@company.com",
                    Email = "hr1@company.com",
                };

                string hr1Password = "6240HR1P";
                var createHr1 = await UserManager.CreateAsync(hr1, hr1Password);
                if (createHr1.Succeeded)
                {
                    await UserManager.AddToRoleAsync(hr1, "HR");
                }
            }

            // find the user with the hr2 email
            var _hr2 = await UserManager.FindByEmailAsync("hr2@company.com");
            if (_hr2 == null)
            {
                var hr2 = new ApplicationUser
                {
                    UserName = "hr2@company.com",
                    Email = "hr2@company.com",
                };

                string hr2Password = "6240HR2P";
                var createHr2 = await UserManager.CreateAsync(hr2, hr2Password);
                if (createHr2.Succeeded)
                {
                    await UserManager.AddToRoleAsync(hr2, "HR");
                }
            }
        }
    }
}
