  using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QRMenu_Mvc.Data;
using QRMenu_Mvc.Models;
using System;

namespace QRMenu_Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<AppUser,AppRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultTokenProviders()
                //AddUserManager<UserManager<AppUser>>()
                //.AddSignInManager<SignInManager<AppUser>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthorization(options => options.AddPolicy("CompAdmin", policy => policy.RequireClaim("BrandId")));
            builder.Services.AddAuthorization(options => options.AddPolicy("RestAdmin", policy => policy.RequireClaim("RestaurantId")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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
           //app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
      
            ApplicationDbContext? context = app.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
            RoleManager<AppRole>? roleManager = app.Services.CreateScope().ServiceProvider.GetService<RoleManager<AppRole>>();
            UserManager<AppUser>? userManager = app.Services.CreateScope().ServiceProvider.GetService<UserManager<AppUser>>();
            DBInitializer dBInitializer = new DBInitializer(context, roleManager, userManager);
            //app.MapControllerRoute();
            //name: "default",
            //pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapRazorPages();

            app.Run();
        }
    }
}