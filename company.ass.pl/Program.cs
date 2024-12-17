using company.ass.BLL;
using company.ass.BLL.interfaces;
using company.ass.BLL.Repositories;
using company.ass.DAL.Data.context;
using company.ass.DAL.models;
using company.ass.pl.mapping;
using company.ass.pl.services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace company.ass.pl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            /*            builder.Services.AddScoped<AppDbcontext>();
            */
            builder.Services.AddDbContext<AppDbcontext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection"));
            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepositories>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitofwork, Unitofwork>();
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbcontext>()
                             .AddDefaultTokenProviders();
            ///////////////life time
            builder.Services.AddScoped<Iscopedservices, scopedservices>();//per request
            builder.Services.AddTransient<Itransientservices, transientservices>();//per operation
            builder.Services.AddSingleton<Isengletionservices, sengletionservices>();//per app

            builder.Services.ConfigureApplicationCookie(config => {
                config.LoginPath = "/Account/SignIn";
               
                });
      
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
