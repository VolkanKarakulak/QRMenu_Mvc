using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QRMenu_Mvc.Models
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 1)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = "";

        [StringLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? Description { get; set; }

        [DisplayName("Görsel")]
        [StringLength(180)]
        public string ImageFileName { get; set; } = "";

        public byte StateId { get; set; }

        [ForeignKey("StateId")]
        public State? State { get; set; }

        public int RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public Restaurant? Restaurant { get; set; }

        //public virtual ICollection<Food> Foods { get; set; }

    }
}
