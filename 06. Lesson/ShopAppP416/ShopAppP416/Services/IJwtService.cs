using ShopAppP416.Models;

namespace ShopAppP416.Services
{
    public interface IJwtService
    {
        string CreateJwt(IConfiguration config, AppUser user, IList<string> roles);
    }
}
