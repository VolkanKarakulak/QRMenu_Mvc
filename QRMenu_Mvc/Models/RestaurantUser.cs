using System.ComponentModel.DataAnnotations.Schema;

namespace QRMenu_Mvc.Models
{
    public class RestaurantUser
    {
        public int RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public Restaurant? Restaurant { get; set; }

        public string UserId { get; set; } = "";

        [ForeignKey("UserId")]
        public AppUser? AppUser { get; set; }
    }
}
