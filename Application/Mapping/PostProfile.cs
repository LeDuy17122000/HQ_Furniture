using Application.DTOs.Post;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User!.FullName));

            CreateMap<PostCreateDto, Post>();

            CreateMap<PostUpdateDto, Post>();

            CreateMap<Post, PostUpdateDto>();
        }
    }
}