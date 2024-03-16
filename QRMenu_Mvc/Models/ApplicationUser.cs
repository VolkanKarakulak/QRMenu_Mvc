using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace QRMenu_Mvc.Models
{
    public class ApplicationUser : IdentityUser
    {
       
        [StringLength(50, MinimumLength = 1)]
        [Column(TypeName = "nvarchar(50)")]
        public override string UserName { get; set; } = "";

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = "";

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SurName { get; set; } = "";

        [EmailAddress]
        [Column(TypeName = "varchar(100)")]
        public override string Email { get; set; } = "";

        [Phone]
        [StringLength(30)]
        [Column(TypeName = "nvarchar(30)")]
        public override string PhoneNumber { get; set; } = "";

        [StringLength(5, MinimumLength = 1)]
        [Column(TypeName = "char(5)")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; } = "";

        [StringLength(200, MinimumLength = 1)]
        [Column(TypeName = "nvarchar(200)")]
        public string Address { get; set; } = "";

        public byte StateId { get; set; }

        public int BrandId { get; set; }

        [ForeignKey("StateId")]
        public State? State { get; set; }

        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }

        //public virtual List<Restaurant>? Restaurants { get; set; }

    }
}
