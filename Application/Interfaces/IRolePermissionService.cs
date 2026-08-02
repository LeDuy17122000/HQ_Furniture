using Application.DTOs.RolePermission;

namespace Application.Interfaces
{
    public interface IRolePermissionService
    {
        Task<List<RolePermissionDto>> GetAllAsync();

        Task<List<RolePermissionViewDto>> GetByRoleAsync(int roleId);

        Task<List<RolePermissionViewDto>> GetByPermissionAsync(int permissionId);

        Task AssignPermissionAsync(RolePermissionAssignDto dto);

        Task RemovePermissionAsync(int roleId, int permissionId);
    }
}