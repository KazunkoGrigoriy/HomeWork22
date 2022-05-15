using HomeWork22.AuthApp;
using HomeWork22.Data;
using HomeWork22.DataContext;
using HomeWork22.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork22
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
            services.AddDbContext<RecordDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<RecordDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IRecord, DataDb>();

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequiredLength = 8;
            });

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account/Login";
                option.LogoutPath = "/Account/Logout";
                option.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            //app.UseAuthorization();
            app.UseStaticFiles();
            app.UseMvc(option =>
            {
                option.MapRoute(
                    name: "default",
                    template: "{controller=Record}/{action=Index}");
            });
        }
    }
}
