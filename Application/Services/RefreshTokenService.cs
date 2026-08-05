using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository refreshRepository;
        private readonly IUserRepository userRepository;
        private readonly IJwtService jwtService;

        public RefreshTokenService(
            IRefreshTokenRepository refreshRepository,
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            this.refreshRepository = refreshRepository;
            this.userRepository = userRepository;
            this.jwtService = jwtService;
        }

        public async Task<LoginResponseDto> GenerateAsync(
            int userId,
            string fullName,
            string email,
            string role)
        {
            var user = await userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found.");

            var accessToken = jwtService.GenerateToken(user);

            var refreshToken = Guid.NewGuid().ToString("N");

            var entity = new RefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken,
                ExpiryDate = DateTime.Now.AddDays(7),
                IsRevoked = false
            };

            await refreshRepository.AddAsync(entity);
            await refreshRepository.SaveAsync();

            return new LoginResponseDto
            {
                UserId = user.UserId,
                FullName = fullName,
                Email = email,
                Role = role,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpireAt = DateTime.Now.AddMinutes(60)
            };
        }

        public async Task<TokenResponseDto?> RefreshAsync(string refreshToken)
        {
            var token = await refreshRepository.GetByTokenAsync(refreshToken);

            if (token == null)
                return null;

            if (token.IsRevoked)
                return null;

            if (token.ExpiryDate < DateTime.Now)
                return null;

            var user = await userRepository.GetDetailByIdAsync(token.UserId);

            if (user == null)
                return null;

            var accessToken = jwtService.GenerateToken(user);

            var newRefresh = Guid.NewGuid().ToString("N");

            token.Token = newRefresh;
            token.ExpiryDate = DateTime.Now.AddDays(7);

            await refreshRepository.UpdateAsync(token);
            await refreshRepository.SaveAsync();

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefresh,
                ExpireAt = DateTime.Now.AddMinutes(60)
            };
        }

        public async Task<bool> RevokeAsync(string refreshToken)
        {
            var token = await refreshRepository.GetByTokenAsync(refreshToken);

            if (token == null)
                return false;

            token.IsRevoked = true;

            await refreshRepository.UpdateAsync(token);
            await refreshRepository.SaveAsync();

            return true;
        }
    }
}