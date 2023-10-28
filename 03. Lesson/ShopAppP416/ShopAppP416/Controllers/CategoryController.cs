using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAppP416.Data;
using ShopAppP416.Dtos.CategoryDtos;
using ShopAppP416.Models;

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
        public IActionResult Get(int page=1, int take=2)
        {
            var query = _context.Categories
                .Where(c => !c.IsDelete);
            CategoryListReturnDto categoryListReturnDto = new();
            categoryListReturnDto.TotalCount = query.Count();
            categoryListReturnDto.Items = query
                .Skip((page-1)*take)
                .Take(take)
                .Select(c=> new CategoryReturnDto
                {
                    Name = c.Name,
                    CreatedAt = c.CreatedAt,
                    UpdateAt = c.UpdateAt,
                    DeletedAt = c.DeletedAt,
                })
                .ToList();
            return Ok(categoryListReturnDto);
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
        [HttpPost]
        public IActionResult Create(CategoryCreateDto category)
        {
            if (_context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower() && !c.IsDelete))
            {
                return BadRequest();
            }
            Category newCategory = new();
            newCategory.Name = category.Name;
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return Ok(201);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryUpdateDto category)
        {
            var existCategory = _context.Categories
                .Where(c=>!c.IsDelete)
                .FirstOrDefault(c => c.Id == id);
            if (existCategory == null) return NotFound();

            if (_context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower() && c.Id != id && !c.IsDelete))
            {
                return BadRequest();
            }
            existCategory.Name = category.Name;
            _context.SaveChanges();
            return Ok(StatusCodes.Status204NoContent);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existCategory = _context.Categories
                .Where (c=>!c.IsDelete)
                .FirstOrDefault(c => c.Id == id);
            if (existCategory == null) return NotFound();
            existCategory.IsDelete = true;
            _context.SaveChanges();
            return Ok(StatusCodes.Status204NoContent);
        }
    }
}
