using ShopAppP416.Models;

namespace ShopAppP416.Dtos.ProductDtos
{
    public class ProductListReturnDto
    {
        public int TotalCount { get; set; }
        public List<ProductReturnDto> Items { get; set; }
        public ProductListReturnDto()
        {
            Items = new List<ProductReturnDto>();
        }
    }
}
