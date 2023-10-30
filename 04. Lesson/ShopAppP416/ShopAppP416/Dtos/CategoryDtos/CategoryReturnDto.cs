namespace ShopAppP416.Dtos.CategoryDtos
{
    public class CategoryReturnDto
    {
        public string Name { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int ProductsCount { get; set; }
    }
}
