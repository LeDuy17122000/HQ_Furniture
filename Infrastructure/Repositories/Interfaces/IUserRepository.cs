using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<List<User>> SearchAsync(string keyword);
        Task<List<User>> GetByRoleAsync(int roleId);
    }
}