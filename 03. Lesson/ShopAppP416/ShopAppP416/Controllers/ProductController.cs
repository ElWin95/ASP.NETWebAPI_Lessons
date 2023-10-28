using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Get(int page=1, int take=3, string search=null)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }
            ProductListReturnDto listReturnDto = new();
            listReturnDto.TotalCount = query.Count();
            listReturnDto.Items = query.Where(p => !p.IsDelete)
                .Skip((page-1)*take)
                .Take(take)
                .Select(p=>new ProductReturnDto
                {
                    Name = p.Name,
                    CostPrice = p.CostPrice,
                    SalePrice = p.SalePrice,
                    CreatedAt = p.CreatedAt,
                    UpdateAt = p.UpdateAt,
                    DeletedAt = p.DeletedAt,
                    Category= new()
                    {
                        Name= p.Category.Name,
                        Id= p.Category.Id,
                        ProductsCount= p.Category.Products.Count,
                    }
                })
                .ToList();
            //foreach (var product in products)
            //{
            //    ProductReturnDto productReturnDto = new();
            //    productReturnDto.Name = product.Name;
            //    productReturnDto.SalePrice = product.SalePrice;
            //    productReturnDto.CostPrice = product.CostPrice;
            //    productReturnDto.CreatedAt = product.CreatedAt;
            //    productReturnDto.UpdateAt = product.UpdateAt;
            //    productReturnDto.DeletedAt = product.DeletedAt;
            //    listReturnDto.Items.Add(productReturnDto);

            //}
            return Ok(listReturnDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int? id)
        {
            if (id == null) return BadRequest();
            var productReturnDto = _context.Products
                .Where(p => !p.IsDelete && p.Id == id)
                .Select(p => new ProductReturnDto
                {
                    Name = p.Name,
                    CostPrice = p.CostPrice,
                    SalePrice = p.SalePrice,
                    CreatedAt = p.CreatedAt,
                    UpdateAt = p.UpdateAt,
                    DeletedAt = p.DeletedAt,
                    Category = new()
                    {
                        Name = p.Category.Name,
                        Id = p.Category.Id,
                        ProductsCount = p.Category.Products.Count,
                    }
                })
                .FirstOrDefault();
            if (productReturnDto == null) return BadRequest();
            
            //var existProduct = _context.Products
            //    .Include(p => p.Category)
            //    .ThenInclude(c=>c.Products)
            //    .FirstOrDefault(p => p.Id == id);
            //var productReturnDto = new ProductReturnDto()
            //{
            //    Name = existProduct.Name,
            //    Category = new()
            //    {
            //        Id = existProduct.Category.Id,
            //        Name = existProduct.Category.Name,
            //        ProductsCount = existProduct.Category.Products.Count,
            //    }
            //};

            return StatusCode(StatusCodes.Status200OK, productReturnDto);
        }
        [HttpPost]
        public IActionResult Create(ProductCreateDto product)
        {
            if(!_context.Categories.Any(c=>c.Id == product.CategoryId && !c.IsDelete)) return BadRequest();

            Product newProduct = new();
            newProduct.Name = product.Name;
            newProduct.SalePrice = product.SalePrice;
            newProduct.CostPrice = product.CostPrice;
            newProduct.CategoryId = product.CategoryId;
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
