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
//using PMS_Admin_Web.Tables;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;
using PMS_Admin_Web.ModelDataAnnotations;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Http;
using PMS_Admin_Web.Models;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace PMS_Admin_Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //var connection = @"Server=(local)\SQLEXPRESS;Database=q8Realtor;Trusted_Connection=True;";
            //string connection = this.Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<q8RealtorContext>(options => options.UseSqlServer(connection));
            
            //services.AddDbContext<q8RealtorContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<RealtorContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddTransient<IDbConnection>((sp) => new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection")));

            //DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute),
            //                                          typeof(RequiredAttributeAdapter));

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddPaging();

            //services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.IgnoreNullValues = true;
                // Add more settings as needed
            });
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
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Home}/{action=Index}/{id?}");
                    pattern: "{controller=Auth}/{action=Login}/{id?}");
                    //pattern: "{controller=Auth}/{action=Login}");
            });
        }
    }
}
