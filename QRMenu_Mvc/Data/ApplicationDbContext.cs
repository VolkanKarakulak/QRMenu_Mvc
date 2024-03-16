using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QRMenu_Mvc.Models;

namespace QRMenu_Mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<QRMenu_Mvc.Models.Brand>? Brand { get; set; }
        public DbSet<QRMenu_Mvc.Models.Category>? Category { get; set; }
        public DbSet<QRMenu_Mvc.Models.Food>? Food { get; set; }
        public DbSet<QRMenu_Mvc.Models.Restaurant>? Restaurant { get; set; }
        public DbSet<QRMenu_Mvc.Models.MainCompany>? MainCompany { get; set; }
    }
}
