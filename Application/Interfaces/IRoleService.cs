using Application.DTOs.Role;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllAsync();

        Task<RoleDto?> GetByIdAsync(int id);

        Task AddAsync(RoleCreateDto dto);

        Task UpdateAsync(RoleUpdateDto dto);

        Task DeleteAsync(int id);
    }
}