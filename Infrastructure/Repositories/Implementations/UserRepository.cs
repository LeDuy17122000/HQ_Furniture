using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class UserRepository
        : Repository<User>,
          IUserRepository
    {
        public UserRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await dbSet
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<User>> GetByRoleAsync(int roleId)
        {
            return await dbSet
                .Where(x => x.RoleId == roleId)
                .ToListAsync();
        }
        public async Task<List<User>> SearchAsync(string keyword)
        {
            return await dbSet
                .Where(x =>
                    x.FullName.Contains(keyword) ||
                    x.Email.Contains(keyword) ||
                    x.Phone.Contains(keyword))
                .ToListAsync();
        }
    }
}