using Microsoft.AspNetCore.Identity;

namespace QRMenu_Mvc.Models
{
    public class AppRole : IdentityRole
    {
      public AppRole(string name):base(name) { }
    }
}
