using AutoMapper;
using ShopAppP416.Dtos.CategoryDtos;
using ShopAppP416.Dtos.ProductDtos;
using ShopAppP416.Models;

namespace ShopAppP416.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryReturnDto>();
                //.ForMember(d => d.TotalProductsCount, map => map.MapFrom(src => src.Products.Count));
            CreateMap<Category, CategoryListReturnDto>();
            CreateMap<Category, CategoryInProductReturnDto>();
            CreateMap<Product, ProductReturnDto>();
        }
    }
}
