using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetAllWithRoleAsync();

        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetDetailByIdAsync(int id);

        Task<List<User>> SearchAsync(string keyword);
        Task ChangeRoleAsync(int userId, int roleId);
    }
}