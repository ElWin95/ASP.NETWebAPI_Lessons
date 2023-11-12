using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAppP416.Data;
using ShopAppP416.Dtos.ProductDtos;
using ShopAppP416.Models;

namespace ShopAppP416.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Get(int page=1, int take=3, string search=null)
        {
            var query = _context.Products.Where(p => !p.IsDelete);
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }
            ProductListReturnDto listReturnDto = new();
            listReturnDto.TotalCount = query.Count();
            var products = query
                .Include(p=>p.Category)
                .Skip((page-1)*take)
                .Take(take)
                .ToList();
            listReturnDto.Items = _mapper.Map<List<ProductReturnDto>>(products);

            return Ok(listReturnDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int? id)
        {
            //if (id == null) return BadRequest();
            var product = _context.Products
                .Include(p => p.Category)
                .Where(p => !p.IsDelete)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return BadRequest();
            var productReturnDto = _mapper.Map<ProductReturnDto>(product);

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
            newProduct.ProductTags = new();
            foreach (var tagId in product.TagIds)
            {
                ProductTag newProductTag = new ();
                newProductTag.TagId = tagId;
                newProductTag.ProductId = newProduct.Id;

                newProduct.ProductTags.Add(newProductTag);
            }
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
