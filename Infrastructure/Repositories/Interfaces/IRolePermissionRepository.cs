using Domain.Models;
using Infrastructure.Repositories;

public interface IRolePermissionRepository : IRepository<RolePermission>
{
    Task<List<RolePermission>> GetAllWithDetailAsync();

    Task<List<RolePermission>> GetByRoleAsync(int roleId);

    Task<List<RolePermission>> GetByPermissionAsync(int permissionId);

    Task<bool> ExistsAsync(int roleId, int permissionId);

    Task AssignAsync(int roleId, List<int> permissionIds);

    Task RemoveAsync(int roleId, int permissionId);
}