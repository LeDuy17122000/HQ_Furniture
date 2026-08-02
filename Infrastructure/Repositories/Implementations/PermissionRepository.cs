using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class PermissionRepository
        : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext context)
            : base(context)
        {
        }

        // Lấy tất cả Permission kèm RolePermission
        public async Task<List<Permission>> GetAllWithRoleAsync()
        {
            return await context.Permissions
                .Include(x => x.RolePermissions)
                    .ThenInclude(rp => rp.Role)
                .ToListAsync();
        }

        // Kiểm tra User có Permission hay không
        public async Task<bool> HasPermissionAsync(
            int userId,
            string permissionName)
        {
            var user = await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
                return false;

            return await context.RolePermissions
                .Include(rp => rp.Permission)
                .AnyAsync(rp =>
                    rp.RoleId == user.RoleId &&
                    rp.Permission!.PermissionName == permissionName);
        }
    }
}