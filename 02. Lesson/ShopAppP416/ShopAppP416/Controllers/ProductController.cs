using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopAppP416.Data;
using ShopAppP416.Dtos.ProductDtos;
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
            var product = _context.Products.Where(p=>!p.IsDelete).FirstOrDefault(p=>p.Id == id);
            if (product == null) return BadRequest();
            return StatusCode(StatusCodes.Status200OK, product);
        }
        [HttpPost]
        public IActionResult Create(ProductCreateDto product)
        {
            Product newProduct = new();
            newProduct.Name = product.Name;
            newProduct.SalePrice = product.SalePrice;
            newProduct.CostPrice = product.CostPrice;
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return Ok(201);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductUpdateDto product)
        {
            if(id != product.Id) return BadRequest();
            var existProduct = _context.Products.FirstOrDefault(x => x.Id == id);
            if (existProduct == null) return BadRequest();
            existProduct.Name = product.Name;
            existProduct.SalePrice = product.SalePrice;
            existProduct.CostPrice = product.CostPrice;
            existProduct.IsDelete = product.IsDelete;
            _context.SaveChanges();
            return Ok(StatusCodes.Status204NoContent);
        }
        [HttpPatch("{id}")] public IActionResult ChangeIsDelete(int id, bool isDelete)
        {
            var existProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if(existProduct == null) return BadRequest();
            existProduct.IsDelete = isDelete;
            _context.SaveChanges();
            return Ok(StatusCodes.Status204NoContent);
        }
    }
}
