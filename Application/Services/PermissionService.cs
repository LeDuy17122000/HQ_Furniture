using Application.DTOs.Permission;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository repository;
        private readonly IMapper mapper;

        public PermissionService(
            IPermissionRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<PermissionDto>> GetAllAsync()
        {
            var data = await repository.GetAllAsync();

            return mapper.Map<List<PermissionDto>>(data);
        }

        public async Task<PermissionDto?> GetByIdAsync(int id)
        {
            var data = await repository.GetByIdAsync(id);

            if (data == null)
                return null;

            return mapper.Map<PermissionDto>(data);
        }

        public async Task AddAsync(PermissionCreateDto dto)
        {
            var permission = mapper.Map<Permission>(dto);

            await repository.AddAsync(permission);

            await repository.SaveAsync();
        }

        public async Task UpdateAsync(PermissionUpdateDto dto)
        {
            var permission = await repository.GetByIdAsync(dto.PermissionId);

            if (permission == null)
                throw new Exception("Permission not found.");

            mapper.Map(dto, permission);

            await repository.UpdateAsync(permission);

            await repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var permission = await repository.GetByIdAsync(id);

            if (permission == null)
                throw new Exception("Permission not found.");

            await repository.DeleteAsync(permission);

            await repository.SaveAsync();
        }
        public async Task<bool> HasPermissionAsync(int userId,string permissionName)
        {
            return await repository.HasPermissionAsync(
                userId,
                permissionName);
        }
    }
}