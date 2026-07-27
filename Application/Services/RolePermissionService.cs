using Application.DTOs.RolePermission;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository repository;

        private readonly IMapper mapper;

        public RolePermissionService(
            IRolePermissionRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<RolePermissionDto>> GetAllAsync()
        {
            var data = await repository.GetAllWithDetailAsync();

            return mapper.Map<List<RolePermissionDto>>(data);
        }

        public async Task<List<RolePermissionDto>> GetByRoleAsync(int roleId)
        {
            var data = await repository.GetByRoleAsync(roleId);

            return mapper.Map<List<RolePermissionDto>>(data);
        }

        public async Task AssignAsync(AssignPermissionDto dto)
        {
            await repository.AssignAsync(dto.RoleId, dto.PermissionIds);
        }

        public async Task RemoveAsync(int roleId, int permissionId)
        {
            await repository.RemoveAsync(roleId, permissionId);
        }
    }
}