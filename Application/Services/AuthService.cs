using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository repository;

        private readonly IJwtService jwtService;

        private readonly PasswordHasher<User> passwordHasher;

        public AuthService(
            IUserRepository repository,
            IJwtService jwtService)
        {
            this.repository = repository;

            this.jwtService = jwtService;

            passwordHasher = new PasswordHasher<User>();
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var exist = await repository.GetAllAsync();

            if (exist.Any(x => x.Email == dto.Email))
                return false;

            var user = new User
            {
                FullName = dto.FullName,

                Email = dto.Email,

                Phone = dto.Phone,

                Address = dto.Address,

                RoleId = 2,

                IsActive = true,

                CreatedDate = DateTime.Now
            };

            user.PasswordHash =
                passwordHasher.HashPassword(user, dto.Password);

            await repository.AddAsync(user);

            await repository.SaveAsync();

            return true;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await repository.GetByEmailAsync(dto.Email);

            if (user == null)
                return null;

            var result =
                passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            var token = jwtService.GenerateToken(user);

            return new LoginResponseDto
            {
                UserId = user.UserId,

                FullName = user.FullName,

                Email = user.Email,

                Role = user.Role?.RoleName ?? "",

                Token = token,

                ExpireAt = DateTime.Now.AddMinutes(60)
            };
        }
    }
}