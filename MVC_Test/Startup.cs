using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Services;
using Microsoft.Extensions.Logging;

namespace MVC_Test
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
            //Sets a default user at random
            IdGenerator.SetRandomUser();
            //
            services.AddAuthentication()
               .AddGoogle(options =>
               {
                   options.ClientId = "778815169745-ku5bq51miqmtqok3qi3eh4dngbmocnn5.apps.googleusercontent.com";
                   options.ClientSecret = "TiMUa8WQUeQrew9omI8z126V";
               });

            //B@rt3r.com
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ItemDBConnection"));
            });
            services.AddRazorPages();
            services.AddControllersWithViews();
            //services.AddScoped<ItemRepository, SQLItemRepository>();
            services.AddScoped<ItemRepository, ManualItemRepo>(_ => new ManualItemRepo(Configuration.GetConnectionString("ItemDBConnection")));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
                
            });
    
        }
        /*
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbConext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ItemDBConnection"));
            });
            services.AddRazorPages();
            services.AddScoped<ItemRepository, SQLItemRepository>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
        */

    }
}  