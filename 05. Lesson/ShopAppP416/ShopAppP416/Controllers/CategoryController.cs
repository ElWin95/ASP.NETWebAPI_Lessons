using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CategoryController(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Get(int page=1, int take=2)
        {
            var query = _context.Categories
                .Where(c => !c.IsDelete);
            CategoryListReturnDto categoryListReturnDto = new();
            categoryListReturnDto.TotalCount = query.Count();
            var categories = query
                .Include(c => c.Products)
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();

            categoryListReturnDto.Items = _mapper.Map<List<CategoryReturnDto>>(categories);

            return Ok(categoryListReturnDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            if (id == null) return BadRequest();
            var existCategory = _context.Categories
                .Include(c => c.Products)
                .Where(c => !c.IsDelete)
                .FirstOrDefault(c => c.Id == id);
            if (existCategory == null) return NotFound();

            var existCategoryReturnDto = _mapper.Map<CategoryReturnDto>(existCategory);
            return Ok(existCategoryReturnDto);
        }
        [HttpPost]
        public IActionResult Create([FromForm]CategoryCreateDto category)
        {
            if (_context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower() && !c.IsDelete))
            {
                return BadRequest();
            }
            Category newCategory = new();
            if (category.Photo.Length>0 && category.Photo.ContentType.Contains("image"))
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(category.Photo.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                using FileStream fileStream = new(path, FileMode.Create);
                category.Photo.CopyTo(fileStream);
                newCategory.ImageUrL = fileName;
            }
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
            existCategory.DeletedAt = DateTime.Now;
            _context.SaveChanges();
            return Ok(StatusCodes.Status204NoContent);
        }
    }
}
