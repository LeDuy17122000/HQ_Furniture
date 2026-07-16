using Application.DTOs.Role;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository repository;
        private readonly IMapper mapper;

        public RoleService(
            IRoleRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<RoleDto>> GetAllAsync()
        {
            var roles = await repository.GetAllAsync();

            return mapper.Map<List<RoleDto>>(roles);
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var role = await repository.GetByIdAsync(id);

            if (role == null)
                return null;

            return mapper.Map<RoleDto>(role);
        }

        public async Task AddAsync(RoleCreateDto dto)
        {
            var role = mapper.Map<Role>(dto);

            await repository.AddAsync(role);

            await repository.SaveAsync();
        }

        public async Task UpdateAsync(RoleUpdateDto dto)
        {
            var role = await repository.GetByIdAsync(dto.RoleId);

            if (role == null)
                throw new Exception("Role not found.");

            mapper.Map(dto, role);

            await repository.UpdateAsync(role);

            await repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await repository.GetByIdAsync(id);

            if (role == null)
                throw new Exception("Role not found.");

            await repository.DeleteAsync(role);

            await repository.SaveAsync();
        }
    }
}