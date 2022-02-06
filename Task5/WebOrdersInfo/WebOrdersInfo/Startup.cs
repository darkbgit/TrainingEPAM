using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.DAL.Repositories.Implementations;
using WebOrdersInfo.Repositories.Interfaces;
using WebOrdersInfo.DAL.Repositories.Implementations.Repositories;
using WebOrdersInfo.Helpers;
using WebOrdersInfo.Mapping;
using WebOrdersInfo.Services.Implementations;
using WebOrdersInfo.Services.Implementations.Mapping;

namespace WebOrdersInfo
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
            services.AddDbContext<WebOrdersInfoContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddHttpContextAccessor();

            services.AddIdentity<User, Role>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 1;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<WebOrdersInfoContext>();

            services.AddTransient<IRepository<Order>, OrdersRepository>();
            services.AddTransient<IRepository<Client>, ClientsRepository>();
            services.AddTransient<IRepository<Manager>, ManagersRepository>();
            services.AddTransient<IRepository<Product>, ProductsRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IOrderService, OrdersService>();
            services.AddTransient<IClientService, ClientsService>();
            services.AddTransient<IProductService, ProductsService>();
            services.AddTransient<IManagerService, ManagerService>();
            services.AddTransient<IFilterService, FilterService>();
            
            services.AddAutoMapper(typeof(MappingProfile), typeof(MappingProfile2));

            services.AddControllersWithViews();

            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            if (env.IsDevelopment())
            {
                CreateRoles(serviceProvider);
                

            }
        }

        private void CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();


            Task<IdentityResult> roleResult;
            var adminRoleName = Configuration["DefaultUsers:AdminRoleName"];

            Task<bool> hasAdminRole = roleManager.RoleExistsAsync(adminRoleName);
            hasAdminRole.Wait();

            if (!hasAdminRole.Result)
            {
                roleResult = roleManager.CreateAsync(new Role(adminRoleName));
                roleResult.Wait();
            }

            var adminEmail = Configuration["DefaultUsers:AdminEmail"];

            Task<User> adminUser = userManager.FindByEmailAsync(adminEmail);
            adminUser.Wait();

            if (adminUser.Result == null)
            {
                User user = new() { Email = adminEmail, UserName = adminEmail };

                var adminPassword = Configuration["DefaultUsers:AdminPassword"];

                Task<IdentityResult> newUser = userManager.CreateAsync(user, adminPassword);
                newUser.Wait();

                if (newUser.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(user, adminRoleName);
                    newUserRole.Wait();
                }
            }

            var userRoleName = Configuration["DefaultUsers:UserRoleName"];

            Task<bool> hasUserRole = roleManager.RoleExistsAsync(userRoleName);
            hasUserRole.Wait();

            if (!hasUserRole.Result)
            {
                roleResult = roleManager.CreateAsync(new Role(userRoleName));
                roleResult.Wait();
            }

            var userEmail = Configuration["DefaultUsers:UserEmail"];

            Task<User> userUser = userManager.FindByEmailAsync(userEmail);
            userUser.Wait();

            if (userUser.Result == null)
            {
                User user = new() { Email = userEmail, UserName = userEmail };

                var userPassword = Configuration["DefaultUsers:UserPassword"];

                Task<IdentityResult> newUser = userManager.CreateAsync(user, userPassword);
                newUser.Wait();

                if (newUser.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(user, userRoleName);
                    newUserRole.Wait();
                }
            }
        }
    }
}
