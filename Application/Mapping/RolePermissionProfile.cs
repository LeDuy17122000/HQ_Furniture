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
                .ForMember(d => d.RoleName,
                    o => o.MapFrom(s => s.Role!.RoleName))

                .ForMember(d => d.PermissionName,
                    o => o.MapFrom(s => s.Permission!.PermissionName));
        }
    }
}