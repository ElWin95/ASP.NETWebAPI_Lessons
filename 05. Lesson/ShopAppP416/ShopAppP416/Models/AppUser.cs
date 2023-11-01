using Microsoft.AspNetCore.Identity;

namespace ShopAppP416.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
