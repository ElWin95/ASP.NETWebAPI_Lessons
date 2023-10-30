using ShopAppP416.Models;

namespace ShopAppP416.Dtos.ProductDtos
{
    public class ProductReturnDto
    {
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public CategoryInProductReturnDto Category { get; set; }
    }
    public class CategoryInProductReturnDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int ProductsCount { get; set; }
    }
}
