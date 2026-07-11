using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class RoleRepository
        : Repository<Role>,
          IRoleRepository
    {
        public RoleRepository(AppDbContext context)
            : base(context)
        {

        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await dbSet
                .FirstOrDefaultAsync(x => x.RoleName == roleName);
        }
    }
}