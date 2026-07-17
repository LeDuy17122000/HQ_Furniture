using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class RolePermissionRepository
        : Repository<RolePermission>,
          IRolePermissionRepository
    {
        public RolePermissionRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<List<RolePermission>> GetByRoleAsync(int roleId)
        {
            return await context.RolePermissions
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .Where(x => x.RoleId == roleId)
                .ToListAsync();
        }
    }
}