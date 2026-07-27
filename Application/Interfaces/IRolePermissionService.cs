using Application.DTOs.RolePermission;

namespace Application.Interfaces
{
    public interface IRolePermissionService
    {
        Task<List<RolePermissionDto>> GetAllAsync();

        Task<List<RolePermissionDto>> GetByRoleAsync(int roleId);

        Task AssignAsync(AssignPermissionDto dto);

        Task RemoveAsync(int roleId, int permissionId);
    }
}