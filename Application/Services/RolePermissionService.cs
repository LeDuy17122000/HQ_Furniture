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

        public async Task<List<RolePermissionViewDto>> GetByRoleAsync(int roleId)
        {
            var data = await repository.GetByRoleAsync(roleId);

            return mapper.Map<List<RolePermissionViewDto>>(data);
        }

        public async Task<List<RolePermissionViewDto>> GetByPermissionAsync(int permissionId)
        {
            var data = await repository.GetByPermissionAsync(permissionId);

            return mapper.Map<List<RolePermissionViewDto>>(data);
        }

        public async Task AssignPermissionAsync(RolePermissionAssignDto dto)
        {
            await repository.AssignAsync(
                dto.RoleId,
                dto.PermissionIds);
        }

        public async Task RemovePermissionAsync(
            int roleId,
            int permissionId)
        {
            await repository.RemoveAsync(
                roleId,
                permissionId);
        }
    }
}