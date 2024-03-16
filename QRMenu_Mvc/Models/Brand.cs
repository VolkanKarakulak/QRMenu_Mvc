using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QRMenu_Mvc.Models
{
    public class Brand
    {
            public int Id { get; set; }

            [StringLength(200, MinimumLength = 2)]
            [Column(TypeName = "nvarchar(200)")]
            public string Name { get; set; } = "";

            [StringLength(5, MinimumLength = 5)]
            [Column(TypeName = "char(5)")]
            [DataType(DataType.PostalCode)]
            public string PostalCode { get; set; } = "";

            [StringLength(200, MinimumLength = 5)]
            [Column(TypeName = "nvarchar(200)")]
            public string Address { get; set; } = "";

            [Phone]
            [StringLength(30)]
            [Column(TypeName = "nvarchar(30)")]
            public string Phone { get; set; } = "";

            [EmailAddress]
            [Column(TypeName = "varchar(100)")]
            public string EMail { get; set; } = "";

            [Column(TypeName = "smalldatetime")]
            public DateTime RegisterDate { get; set; }

            [StringLength(11, MinimumLength = 10)]
            [Column(TypeName = "varchar(11)")]
            public string TaxNumber { get; set; } = "";

            [StringLength(100)]
            [Column(TypeName = "varchar(100)")]
            public string? WebbAddress { get; set; }

            public byte StateId { get; set; }

            [ForeignKey("StateId")]
            public State? State { get; set; }

            public List<Restaurant>? Restaurants { get; set; }
        }
}
