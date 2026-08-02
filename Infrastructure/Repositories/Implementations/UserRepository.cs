using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class UserRepository
        : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<List<User>> GetAllWithRoleAsync()
        {
            return await context.Users
                .Include(x => x.Role)
                .ToListAsync();
        }

        public async Task<User?> GetDetailByIdAsync(int id)
        {
            return await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<List<User>> SearchAsync(string keyword)
        {
            return await context.Users
                .Include(x => x.Role)
                .Where(x =>
                    x.FullName.Contains(keyword) ||
                    x.Email.Contains(keyword))
                .ToListAsync();
        }
        public async Task ChangeRoleAsync(int userId, int roleId)
        {
            var user = await context.Users.FindAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            user.RoleId = roleId;

            context.Users.Update(user);

            await context.SaveChangesAsync();
        }
    }
}