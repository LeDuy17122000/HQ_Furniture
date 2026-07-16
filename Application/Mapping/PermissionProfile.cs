using Application.DTOs.Permission;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionDto>();

            CreateMap<PermissionCreateDto, Permission>();

            CreateMap<PermissionUpdateDto, Permission>();

            CreateMap<Permission, PermissionUpdateDto>();
        }
    }
}