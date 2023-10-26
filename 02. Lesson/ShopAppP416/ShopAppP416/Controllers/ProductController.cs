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
            var query = _context.Products.AsQueryable();
            ProductListReturnDto listReturnDto = new();
            listReturnDto.TotalCount = query.Count();
            foreach (var product in query.Where(p => !p.IsDelete).ToList())
            {
                ProductReturnDto productReturnDto = new();
                productReturnDto.Name = product.Name;
                productReturnDto.SalePrice = product.SalePrice;
                productReturnDto.CostPrice = product.CostPrice;
                productReturnDto.CreatedAt = product.CreatedAt;
                productReturnDto.UpdateAt = product.UpdateAt;
                productReturnDto.DeletedAt = product.DeletedAt;
                listReturnDto.Items.Add(productReturnDto);

            }
            return Ok(listReturnDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int? id)
        {
            if (id == null) return BadRequest();
            var productReturnDto = _context.Products
                .Where(p => !p.IsDelete && p.Id == id)
                .Select(p=>new ProductReturnDto
                {
                    Name = p.Name,
                    CostPrice = p.CostPrice,
                    SalePrice = p.SalePrice,
                    CreatedAt = p.CreatedAt,
                    UpdateAt = p.UpdateAt,
                    DeletedAt = p.DeletedAt,
                })
                .FirstOrDefault();
            if (productReturnDto == null) return BadRequest();

            return StatusCode(StatusCodes.Status200OK, productReturnDto);
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
