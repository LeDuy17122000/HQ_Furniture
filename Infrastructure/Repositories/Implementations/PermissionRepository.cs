using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.Implementations
{
    public class PermissionRepository
        : Repository<Permission>,
          IPermissionRepository
    {
        public PermissionRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}