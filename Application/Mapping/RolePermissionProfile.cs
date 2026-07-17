using Application.DTOs.RolePermission;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class RolePermissionProfile : Profile
    {
        public RolePermissionProfile()
        {
            CreateMap<RolePermission, RolePermissionDto>()
                .ForMember(x => x.RoleName,
                    opt => opt.MapFrom(s => s.Role!.RoleName))
                .ForMember(x => x.PermissionName,
                    opt => opt.MapFrom(s => s.Permission!.PermissionName));
        }
    }
}