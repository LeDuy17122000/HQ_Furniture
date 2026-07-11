using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;

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
    }
}