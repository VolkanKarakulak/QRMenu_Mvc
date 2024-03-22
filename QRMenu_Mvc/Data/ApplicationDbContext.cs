using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QRMenu_Mvc.Models;

namespace QRMenu_Mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Brand>? Brand { get; set; }
        public DbSet<Category>? Category { get; set; }
        public DbSet<State>? State { get; set; }
        public DbSet<Food>? Food { get; set; }
        public DbSet<Restaurant>? Restaurant { get; set; }
        public DbSet<MainCompany>? MainCompany { get; set; }

        
    }
}
