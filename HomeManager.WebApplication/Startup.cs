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
using HomeManager.Data;
using Microsoft.EntityFrameworkCore;
using HomeManager.Data.Repositories;
using HomeManager.Data.Repositories.Interfaces;
using HomeManager.Services;
using HomeManager.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using HomeManager.Models;

namespace HomeManager.WebApplication
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
            services.AddDbContext<HomeManagerContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("HomeManagerDbConnection"), b => b.MigrationsAssembly("HomeManager.Data")));

            services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<HomeManagerContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IPayment_TemplateRepository, Payment_TemplateRepository>();

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IPayment_TemplateService, Payment_TemplateService>();

            services.AddControllersWithViews();
            services.AddMvc();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
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
                endpoints.MapRazorPages();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<HomeManagerContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
