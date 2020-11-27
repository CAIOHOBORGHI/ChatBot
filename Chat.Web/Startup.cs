using Chat.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.DataProtection;
using Chat.Database.Models;

namespace Chat.Web
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
            services.AddControllersWithViews();

            string connectionString = GetDatabaseConnectionString();
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(connectionString)
            );

            services.AddControllersWithViews();

            services.AddDefaultIdentity<ChatUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Stores identity keys in database
            services.AddDataProtection()
                .PersistKeysToDbContext<ApplicationDbContext>();
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
            }
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
          
        private string GetDatabaseConnectionString()
        {
            // The next lines will help us to connect to a docker database instance
            string connectionString = "";

            string database = Configuration["DBDATABASE"];
            string host = Configuration["DBHOST"];
            string password = Configuration["DBPASSWORD"];
            string port = Configuration["DBPORT"];
            string user = Configuration["DBUSER"];

            // If any of the variables is null, get connectionString from appSettings.json
            if (new List<string>() { database, host, password, port }.Any(s => s == null))
            {
                connectionString = Configuration.GetConnectionString("DatabaseConnection");
            }
            else
            {
                connectionString = $"Server={host}, {port};Database={database};User Id={user};Password={password};";
            }
            return connectionString;
        }
    
    }
}
