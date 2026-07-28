using Application.DTOs.ProductImage;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class ProductImageProfile : Profile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImage, ProductImageDto>()
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product!.ProductName));

            CreateMap<ProductImageCreateDto, ProductImage>();

            CreateMap<ProductImageUpdateDto, ProductImage>();

            CreateMap<ProductImage, ProductImageUpdateDto>();
        }
    }
}