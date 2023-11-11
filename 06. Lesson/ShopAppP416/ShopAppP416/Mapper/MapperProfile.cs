using AutoMapper;
using ShopAppP416.Dtos.CategoryDtos;
using ShopAppP416.Dtos.ProductDtos;
using ShopAppP416.Helpers;
using ShopAppP416.Models;

namespace ShopAppP416.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryReturnDto>().ReverseMap()
                .ForMember(ds => ds.ImageUrL, map => map.MapFrom(src => "http://localhost:5278/img/" + src.ImageUrl));
                //.ForMember(d => d.TotalProductsCount, map => map.MapFrom(src => src.Products.Count));
            CreateMap<Category, CategoryListReturnDto>();
            CreateMap<Category, CategoryInProductReturnDto>();
            CreateMap<Product, ProductReturnDto>()
                .ForMember(ds=>ds.Profit, map=>map.MapFrom(src=>src.SalePrice - src.CostPrice))
                .ForMember(ds=>ds.Day, map=>map.MapFrom(src=>src.CreatedAt.CalculateDay()));
        }
    }
}
