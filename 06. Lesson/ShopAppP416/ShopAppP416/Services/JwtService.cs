using Microsoft.IdentityModel.Tokens;
using ShopAppP416.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopAppP416.Services
{
    public class JwtService : IJwtService
    {
        public string CreateJwt(IConfiguration _config, AppUser user, IList<string> roles)
        {
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var claims = new List<Claim>()
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email)
             };
            claims.AddRange(roles.Select(r=> new Claim(ClaimTypes.Role, r)).ToList());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
