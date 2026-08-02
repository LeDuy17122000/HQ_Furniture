using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<List<Permission>> GetAllWithRoleAsync();
        Task<bool> HasPermissionAsync(int userId, string permissionName);
       
    }
}