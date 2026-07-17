using Application.DTOs.RolePermission;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository repository;
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public RolePermissionService(
            IRolePermissionRepository repository,
            AppDbContext context,
            IMapper mapper)
        {
            this.repository = repository;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<RolePermissionDto>> GetAllAsync()
        {
            var data = await context.RolePermissions
                .Include(x => x.Role)
                .Include(x => x.Permission)
                .ToListAsync();

            return mapper.Map<List<RolePermissionDto>>(data);
        }

        public async Task<List<RolePermissionDto>> GetByRoleAsync(int roleId)
        {
            var data = await repository.GetByRoleAsync(roleId);

            return mapper.Map<List<RolePermissionDto>>(data);
        }

        public async Task AssignAsync(AssignPermissionDto dto)
        {
            foreach (var permissionId in dto.PermissionIds)
            {
                bool exists = await context.RolePermissions.AnyAsync(x =>
                    x.RoleId == dto.RoleId &&
                    x.PermissionId == permissionId);

                if (!exists)
                {
                    await context.RolePermissions.AddAsync(new RolePermission
                    {
                        RoleId = dto.RoleId,
                        PermissionId = permissionId
                    });
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int roleId, int permissionId)
        {
            var item = await context.RolePermissions.FindAsync(roleId, permissionId);

            if (item == null)
                return;

            context.RolePermissions.Remove(item);

            await context.SaveChangesAsync();
        }
    }
}