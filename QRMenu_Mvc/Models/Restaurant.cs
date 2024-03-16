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

        [StringLength(5, MinimumLength = 5)]
        [Column(TypeName = "char(5)")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; } = "";

        [StringLength(200, MinimumLength = 5)]
        [Column(TypeName = "nvarchar(200)")]
        public string AddressDetail { get; set; } = "";

        [Column(TypeName = "smalldatetime")]
        public DateTime DateTime { get; set; }

        public byte StateId { get; set; }

        [ForeignKey("StateId")]
        public State? State { get; set; }

        public int MainCompanyId { get; set; }

        [ForeignKey("MainCompanyId")]
        public MainCompany? MainCompany { get; set; }

        public virtual List<User>? Users { get; set; }

        //public List<Category>? Categories { get; set; }
    }
}
