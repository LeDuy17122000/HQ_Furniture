using Application.DTOs.RolePermission;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class RolePermissionProfile : Profile
    {
        public RolePermissionProfile()
        {
            // DTO thường
            CreateMap<RolePermission, RolePermissionDto>()
                .ReverseMap();

            // DTO hiển thị
            CreateMap<RolePermission, RolePermissionViewDto>()
                .ForMember(dest => dest.RoleName,
                    opt => opt.MapFrom(src => src.Role!.RoleName))
                .ForMember(dest => dest.PermissionName,
                    opt => opt.MapFrom(src => src.Permission!.PermissionName));
        }
    }
}