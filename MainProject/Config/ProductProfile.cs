using AutoMapper;
using Domain.Entities;
using MainProject.ProductDtos;

namespace MainProject.Config
{
    public class ProductProfile : Profile
    {
            public ProductProfile()
            {
                CreateMap<Product, ProductDto>();
           // CreateMap<TSource, TDestination>();
                CreateMap<Shop, ShopDto>();
        }
        
    }
}
