using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QRMenu_Mvc.Models;


namespace QRMenu_Mvc.Data
{
    public class DBInitializer
    {
        public DBInitializer(ApplicationDbContext? context, RoleManager<AppRole>? roleManager, UserManager<AppUser>? userManager)
        {
            State state;
            AppRole appRole;
            AppUser appUser;
            Brand? brand = null;

            if (context != null)
            {
                context.Database.Migrate();
                if (context.State.Count() == 0)
                {
                    state = new State();
                    state.Id = 0;
                    state.Name = "Deleted";
                    context.State.Add(state);
                    state = new State();
                    state.Id = 1;
                    state.Name = "Active";
                    context.State.Add(state);
                    state = new State();
                    state.Id = 2;
                    state.Name = "Passive";
                    context.State.Add(state);
                }
                if (context.Brand.Count() == 0)
                {
                    brand = new Brand();
                    brand.Address = "adres";
                    brand.EMail = "abc@def.com";
                    brand.Name = "Brand";
                    brand.Phone = "1112223344";
                    brand.PostalCode = "12345";
                    brand.RegisterDate = DateTime.Today;
                    brand.StateId = 1;
                    brand.TaxNumber = "11111111111";
                    context.Brand.Add(brand);
                }
                context.SaveChanges();
                if (roleManager != null)
                {
                    if (roleManager.Roles.Count() == 0)
                    {
                        appRole = new AppRole("Admin");
                        roleManager.CreateAsync(appRole).Wait();
                        appRole = new AppRole("BrandAdmin");
                        roleManager.CreateAsync(appRole).Wait();
                        appRole = new AppRole("RestaurantAdmin");
                        roleManager.CreateAsync(appRole).Wait();
                    }
                }
                if (userManager != null)
                {
                    if (userManager.Users.Count() == 0)
                    {
                        if (brand != null)
                        {
                            appUser = new AppUser();
                            appUser.UserName = "Admin";
                            appUser.BrandId = brand.Id;
                            appUser.Name = "Admin";
                            appUser.Email = "abc@def.com";
                            appUser.PhoneNumber = "1112223344";
                            appUser.RegisterDate = DateTime.Today;
                            appUser.StateId = 1;
                            userManager.CreateAsync(appUser, "Admin123!").Wait();
                            userManager.AddToRoleAsync(appUser, "Admin").Wait();
                        }
                    }
                }
            }
        }
    }
}