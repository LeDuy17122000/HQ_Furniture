using Application.DTOs.User;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public UserService(
            IUserRepository repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await repository.GetAllAsync();

            return mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await repository.GetByIdAsync(id);

            if (user == null)
                return null;

            return mapper.Map<UserDto>(user);
        }

        public async Task AddAsync(UserCreateDto dto)
        {
            var user = mapper.Map<User>(dto);

            await repository.AddAsync(user);
            await repository.SaveAsync();
        }

        public async Task UpdateAsync(UserUpdateDto dto)
        {
            var user = await repository.GetByIdAsync(dto.UserId);

            if (user == null)
                throw new Exception("User not found.");

            mapper.Map(dto, user);

            await repository.UpdateAsync(user);
            await repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await repository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("User not found.");

            await repository.DeleteAsync(user);
            await repository.SaveAsync();
        }
    }
}