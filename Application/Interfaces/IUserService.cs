using Application.DTOs.User;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();

        Task<UserDto?> GetByIdAsync(int id);

        Task AddAsync(UserCreateDto dto);

        Task UpdateAsync(UserUpdateDto dto);

        Task DeleteAsync(int id);
        Task ChangeRoleAsync(UserChangeRoleDto dto);
    }
}