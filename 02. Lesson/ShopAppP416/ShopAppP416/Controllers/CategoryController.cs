using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAppP416.Data;
using ShopAppP416.Dtos.CategoryDtos;

namespace ShopAppP416.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _context.Categories
                .Where(c=>!c.IsDelete)
                .ToList();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            if (id == null) return BadRequest();
            var existCategoryReturnDto = _context.Categories
                .Where (c=>!c.IsDelete && c.Id == id)
                .Select(c=> new CategoryReturnDto
                {
                    Name = c.Name,
                    CreatedAt = c.CreatedAt,
                    DeletedAt = c.DeletedAt,
                    UpdateAt = c.UpdateAt
                })
                .FirstOrDefault();
            if (existCategoryReturnDto == null) return NotFound();
            return Ok(existCategoryReturnDto);
        }
    }
}
