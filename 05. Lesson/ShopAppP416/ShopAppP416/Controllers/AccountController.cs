using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopAppP416.Dtos.AccountDtos;
using ShopAppP416.Models;

namespace ShopAppP416.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null) return StatusCode(409);
            user = new AppUser
            {
                UserName = registerDto.UserName,
                FullName = registerDto.FullName,
                Email = registerDto.Email,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(201);
        }

        [HttpGet]
        public async Task<IActionResult> AddRole()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
            return Ok(201);
        }
    }
}
