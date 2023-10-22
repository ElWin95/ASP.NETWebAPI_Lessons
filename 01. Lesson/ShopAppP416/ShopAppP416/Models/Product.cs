namespace ShopAppP416.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public bool IsDelete { get; set; }
    }
}
