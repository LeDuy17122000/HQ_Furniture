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
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly PasswordHasher<User> passwordHasher;

        public AuthService(
            IUserRepository repository,
            IJwtService jwtService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            this.repository = repository;
            this.jwtService = jwtService;
            this.refreshTokenRepository = refreshTokenRepository;

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

            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            // Generate Access Token
            var accessToken = jwtService.GenerateToken(user);

            // Generate Refresh Token
            var refreshToken = Guid.NewGuid().ToString("N");

            // Lưu Refresh Token vào Database
            await refreshTokenRepository.AddAsync(new RefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken,
                ExpiryDate = DateTime.Now.AddDays(7),
                IsRevoked = false
            });

            await refreshTokenRepository.SaveAsync();

            return new LoginResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role?.RoleName ?? "",
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpireAt = DateTime.Now.AddMinutes(60)
            };
        }
    }
}