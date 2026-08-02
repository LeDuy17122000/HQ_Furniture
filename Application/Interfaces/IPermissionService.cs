using Application.DTOs.Permission;

namespace Application.Interfaces
{
    public interface IPermissionService
    {
        Task<List<PermissionDto>> GetAllAsync();

        Task<PermissionDto?> GetByIdAsync(int id);

        Task AddAsync(PermissionCreateDto dto);

        Task UpdateAsync(PermissionUpdateDto dto);

        Task DeleteAsync(int id);
        Task<bool> HasPermissionAsync(int userId, string permissionName);
    }
}