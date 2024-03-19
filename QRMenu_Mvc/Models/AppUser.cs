using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace QRMenu_Mvc.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = "";

        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string SurName { get; set; } = "";

        [StringLength(5, MinimumLength = 1)]
        [Column(TypeName = "char(5)")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; } = "";


        [StringLength(200, MinimumLength = 1)]
        [Column(TypeName = "nvarchar(200)")]
        public string Address { get; set; } = "";

        public byte StateId { get; set; }

        [ForeignKey("StateId")]
        public State? State { get; set; }

        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }
        public virtual List<Restaurant>? Restaurants { get; set; }
    }
}

