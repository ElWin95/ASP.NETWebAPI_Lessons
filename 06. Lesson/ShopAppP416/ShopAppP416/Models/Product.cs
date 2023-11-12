namespace ShopAppP416.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductTag> ProductTags { get; set; }
    }
}
