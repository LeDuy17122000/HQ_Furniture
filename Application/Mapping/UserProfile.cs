using Application.DTOs.User;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest.RoleName,
                    opt => opt.MapFrom(src => src.Role!.RoleName));

            CreateMap<UserCreateDto, User>();

            CreateMap<UserUpdateDto, User>();

            CreateMap<User, UserUpdateDto>();
        }
    }
}