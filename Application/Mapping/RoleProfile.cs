using Application.DTOs.Role;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();

            CreateMap<RoleCreateDto, Role>();

            CreateMap<RoleUpdateDto, Role>();

            CreateMap<Role, RoleUpdateDto>();
        }
    }
}