using Application.DTOs.Review;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            // Entity -> DTO
            CreateMap<Review, ReviewDto>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User!.FullName))
                .ForMember(
                    dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product!.ProductName));

            // DTO -> Entity
            CreateMap<ReviewCreateDto, Review>();

            CreateMap<ReviewUpdateDto, Review>();

            // Entity -> UpdateDto
            CreateMap<Review, ReviewUpdateDto>();
        }
    }
}