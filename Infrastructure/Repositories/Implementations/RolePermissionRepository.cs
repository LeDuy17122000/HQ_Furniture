using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class RolePermissionRepository
        : Repository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<List<RolePermission>> GetAllWithDetailAsync()
        {
            return await context.RolePermissions
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .ToListAsync();
        }

        public async Task<List<RolePermission>> GetByRoleAsync(int roleId)
        {
            return await context.RolePermissions
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .Where(x => x.RoleId == roleId)
                .ToListAsync();
        }

        public async Task AssignAsync(int roleId, List<int> permissionIds)
        {
            foreach (var permissionId in permissionIds)
            {
                bool exists = await context.RolePermissions.AnyAsync(x =>
                    x.RoleId == roleId &&
                    x.PermissionId == permissionId);

                if (!exists)
                {
                    await context.RolePermissions.AddAsync(new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permissionId
                    });
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int roleId, int permissionId)
        {
            var entity = await context.RolePermissions
                .FirstOrDefaultAsync(x =>
                    x.RoleId == roleId &&
                    x.PermissionId == permissionId);

            if (entity != null)
            {
                context.RolePermissions.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}