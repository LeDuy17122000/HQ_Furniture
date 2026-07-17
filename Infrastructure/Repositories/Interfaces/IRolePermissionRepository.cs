using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IRolePermissionRepository
        : IRepository<RolePermission>
    {
        Task<List<RolePermission>> GetAllWithDetailAsync();

        Task<List<RolePermission>> GetByRoleAsync(int roleId);

        Task AssignAsync(int roleId, List<int> permissionIds);

        Task RemoveAsync(int roleId, int permissionId);
    }
}