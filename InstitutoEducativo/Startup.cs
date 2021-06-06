using InstitutoEducativo.Data;
using InstitutoEducativo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo
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
            
            if(Configuration.GetValue<bool>("DbInMem"))
            {
                services.AddDbContext<DbContextInstituto>(options => options.UseInMemoryDatabase("InstitutoEducativo"));
            }
            else
            {
                services.AddDbContext<DbContextInstituto>(options => options.UseSqlServer(Configuration.GetConnectionString("InstitutoEducativoCS")));
            }
            //Creo tabla intermedia entre Persona y Rol

            services.AddIdentity<Persona,Rol>().AddEntityFrameworkStores<DbContextInstituto>();

            services.Configure<IdentityOptions>(options => options.Password.RequireNonAlphanumeric = false);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbContextInstituto miContexto)
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


            if (!Configuration.GetValue<bool>("DbInMem"))
            {
                miContexto.Database.Migrate();// --> asegura la base de datos y ejecuta todas las migraciones
            }
            
            
    
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
