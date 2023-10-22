using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAppP416.Data;
using ShopAppP416.Models;

namespace ShopAppP416.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Products.Where(p=>!p.IsDelete).ToList());
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int? id)
        {
            if (id == null) return BadRequest();
            var product = _context.Products.FirstOrDefault(p=>p.Id == id);
            if (product == null) return BadRequest();
            return StatusCode(StatusCodes.Status200OK, product);
        }
    }
}
