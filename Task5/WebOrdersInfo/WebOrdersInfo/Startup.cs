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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WebOrdersInfo.Core.Services.Interfaces;
using WebOrdersInfo.DAL.Core;
using WebOrdersInfo.DAL.Core.Entities;
using WebOrdersInfo.DAL.Repositories.Implementations;
using WebOrdersInfo.Repositories.Interfaces;
using WebOrdersInfo.DAL.Repositories.Implementations.Repositories;
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

            //services.AddSingleton<IUriService>(o =>
            //{
            //    var accessor = o.GetRequiredService<IHttpContextAccessor>();
            //    var request = accessor.HttpContext.Request;
            //    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
            //    return new UriService(uri);
            //});

            services.AddIdentity<User, Role>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<WebOrdersInfoContext>();

            services.AddTransient<IRepository<Order>, OrdersRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IOrderService, OrdersService>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
