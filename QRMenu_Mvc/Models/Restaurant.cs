using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QRMenu_Mvc.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        [Phone]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(30)")]
        public string Phone { get; set; } = "";

        [StringLength(5, MinimumLength = 1)]
        [Column(TypeName = "char(5)")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; } = "";

        [StringLength(200, MinimumLength = 1)]
        [Column(TypeName = "nvarchar(200)")]
        public string AddressDetail { get; set; } = "";

        [Column(TypeName = "smalldatetime")]
        public DateTime DateTime { get; set; }

        public byte StateId { get; set; }

        [ForeignKey("StateId")]
        public State? States { get; set; }

        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }

        public virtual List<AppUser>? AppUsers { get; set; }

        //public List<Category>? Categories { get; set; }
    }
}
