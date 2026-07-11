using AutoMapper;
using Domain.Models;
using Application.DTOs.Product;

namespace Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListDto>()
                .ForMember(
                    d => d.CategoryName,
                    o => o.MapFrom(s => s.Category!.CategoryName));

            CreateMap<Product, ProductDetailDto>()
                .ForMember(
                    d => d.CategoryName,
                    o => o.MapFrom(s => s.Category!.CategoryName));

            CreateMap<ProductCreateDto, Product>();

            CreateMap<ProductUpdateDto, Product>();

            CreateMap<Product, ProductUpdateDto>();
        }
    }
}