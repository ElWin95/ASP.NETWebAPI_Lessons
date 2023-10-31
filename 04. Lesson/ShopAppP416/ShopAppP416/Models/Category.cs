namespace ShopAppP416.Models
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrL { get; set; }
        public List<Product> Products { get; set; }
    }
}
